using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMaze.DbStuff;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.UserAccount;
using WebMaze.DbStuff.Model.Medicine;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Repository.MedicineRepository;
using WebMaze.Models.Account;
using WebMaze.Models.Department;
using WebMaze.Models.Bus;
using WebMaze.Models.Certificates;
using WebMaze.Models.HealthDepartment;
using WebMaze.Models.UserTasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using WebMaze.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebMaze.Models.Police;
using WebMaze.DbStuff.Model.Police;
using WebMaze.DbStuff.Repository.MedicineRepo;
using WebMaze.Models.Roles;
using WebMaze.Models.Police.Violation;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using WebMaze.Infrastructure;
using WebMaze.Models.HDDoctor;
using WebMaze.Models.HDManager;
using WebMaze.DbStuff.Repository.Life;
using WebMaze.DbStuff.Model.Life;
using WebMaze.DbStuff.Service.Life;
using WebMaze.Models.Life;
using WebMaze.Models.Transactions;

namespace WebMaze
{
    public class Startup
    {
        public const string AuthMethod = "CoockieAuth";
        public const string PoliceAuthMethod = "PoliceAuth";
        public const string MedicineAuth = "CookieMedicineAuth";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=WebMazeKz;Trusted_Connection=True;MultipleActiveResultSets=true;";
            services.AddDbContext<WebMazeContext>(option => option.UseSqlServer(connectionString));

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "User.Auth";
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                })
                .AddCookie(PoliceAuthMethod, config =>
                {
                    config.Cookie.Name = "PUser";
                    config.LoginPath = "/Police/Login";
                });

            services.AddAuthentication(AuthMethod)
                .AddCookie(MedicineAuth, config =>
                {
                    config.Cookie.Name = "Med.Auth";
                    config.LoginPath = "/HealthDepartment/Login";
                    config.AccessDeniedPath = "/HealthDepartment/AccessDenied";
                });

            services.AddTransient<IAuthorizationHandler, RestrictAccessToBlockedUsersHandler>(s =>
                new RestrictAccessToBlockedUsersHandler(s.GetService<CitizenUserRepository>()));

            services.AddTransient<IAuthorizationHandler, RestrictAccessToDeadUsersHandler>(s =>
                new RestrictAccessToDeadUsersHandler(s.GetService<CitizenUserRepository>()));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admins", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                    policy.Requirements.Add(new RestrictAccessToBlockedUsersRequirement());
                    policy.Requirements.Add(new RestrictAccessToDeadUsersRequirement());
                });
            });

            RegistrationMapper(services);

            RegistrationRepository(services);

            services.AddScoped(s => new UserValidator(
                s.GetService<CitizenUserRepository>(), 
                requiredPasswordLength:3));

            services.AddScoped(s => new UserService(s.GetService<CitizenUserRepository>(),
                s.GetService<RoleRepository>(),
                s.GetService<IHttpContextAccessor>()));

            services.AddScoped(s => new TransactionService(s.GetService<TransactionRepository>(),
                s.GetService<CitizenUserRepository>()));

            services.AddScoped(s => new LifeService(s.GetService<CitizenUserRepository>(),
                s.GetService<AdressRepository>(),
                s.GetService<RoleRepository>()));

            services.AddHttpContextAccessor();

            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddHttpClient<CertificateService>();

        }

        private void RegistrationMapper(IServiceCollection services)
        {
            var configurationExpression = new MapperConfigurationExpression();

            configurationExpression.CreateMap<CitizenUser, ProfileViewModel>();
            configurationExpression.CreateMap<ProfileViewModel, CitizenUser>();

            configurationExpression.CreateMap<CitizenUser, RegistrationViewModel>();
            configurationExpression.CreateMap<RegistrationViewModel, CitizenUser>();

            configurationExpression.CreateMap<Adress, AdressViewModel>();
            configurationExpression.CreateMap<AdressViewModel, Adress>();

            configurationExpression.CreateMap<HealthDepartment, HealthDepartmentViewModel>();
            configurationExpression.CreateMap<HealthDepartmentViewModel, HealthDepartment>();

            configurationExpression.CreateMap<Bus, BusViewModel>();
            configurationExpression.CreateMap<BusViewModel, Bus>();

            configurationExpression.CreateMap<BusRoute, CreateBusRouteViewModel>();
            configurationExpression.CreateMap<CreateBusRouteViewModel, BusRoute>();

            configurationExpression.CreateMap<BusWorker, ManageBusWorkerViewModel>();
            configurationExpression.CreateMap<ManageBusWorkerViewModel, BusWorker>();

            configurationExpression.CreateMap<BusOrder, BusOrderViewModel>();
            configurationExpression.CreateMap<BusOrderViewModel, BusOrder>();

            configurationExpression.CreateMap<BusRouteTime, BusRouteTimeViewModel>();
            configurationExpression.CreateMap<BusRouteTimeViewModel, BusRouteTime>();

            configurationExpression.CreateMap<Bus, BusOrderViewModel>();
            configurationExpression.CreateMap<BusOrderViewModel, Bus>();

            configurationExpression.CreateMap<RecordForm, RecordFormViewModel>();
            configurationExpression.CreateMap<RecordFormViewModel, RecordForm>();

            configurationExpression.CreateMap<RecordForm, ListRecordFormViewModel>();
            configurationExpression.CreateMap<ListRecordFormViewModel, RecordForm>();

            configurationExpression.CreateMap<UserTask, UserTaskViewModel>();
            configurationExpression.CreateMap<UserTaskViewModel, UserTask>();

            #region Life project
            configurationExpression.CreateMap<Accident, AccidentViewModel>()
                .ForMember(
                    destination => destination.AccidentCategoryText,
                    opt => opt.MapFrom(source => Dictionaries.GetText(source.AccidentCategory)))
                .ForMember(
                    destination => destination.SelectedAccidentCategory,
                    opt => opt.MapFrom(source => source.AccidentCategory))
                .ForMember(
                    destination => destination.SelectedAccidentAddress,
                    opt => opt.MapFrom(source => source.AccidentAddress.Id))
                .ForMember(
                    destination => destination.AccidentAddressText,
                    opt => opt.MapFrom(source => source.AccidentAddress == null ? Dictionaries.NotAvailable : $"г.{source.AccidentAddress.City}, ул.{source.AccidentAddress.Street}, {source.AccidentAddress.HouseNumber}"))
                .ForMember(
                    destination => destination.AccidentDescription,
                    opt => opt.MapFrom(source => source.AccidentDescription ?? Dictionaries.NotAvailable));
            configurationExpression.CreateMap<AccidentViewModel, Accident>();

            configurationExpression.CreateMap<Accident, AccidentDetailsViewModel>()
                .ForMember(
                    destination => destination.AccidentCategoryText,
                    opt => opt.MapFrom(source => Dictionaries.GetText(source.AccidentCategory)))
                .ForMember(
                    destination => destination.SelectedAccidentCategory,
                    opt => opt.MapFrom(source => source.AccidentCategory));

            configurationExpression.CreateMap<FireDetail, FireDetailViewModel>()
                    .ForMember(
                    destination => destination.FireCauseText,
                    opt => opt.MapFrom(source => Dictionaries.GetText(source.FireCause ?? FireCauseEnum.NotAvailable)))
                .ForMember(
                    destination => destination.FireClassText,
                    opt => opt.MapFrom(source => Dictionaries.GetText(source.FireClass ?? FireClassEnum.NotAvailable)));
            configurationExpression.CreateMap<FireDetailViewModel, FireDetail>();

            configurationExpression.CreateMap<AccidentVictim, AccidentVictimViewModel>()
                .ForMember(
                    destination => destination.VictimName,
                    opt => opt.MapFrom(source => $"{source.Victim.FirstName} {source.Victim.LastName}"))
                .ForMember(
                    destination => destination.CitizenId,
                    opt => opt.MapFrom(source => source.Victim.Id))
                .ForMember(
                    destination => destination.InitialCitizenId,
                    opt => opt.MapFrom(source => source.Victim.Id))
                .ForMember(
                    destination => destination.AccidentId,
                    opt => opt.MapFrom(source => source.Accident.Id))
                .ForMember(
                    destination => destination.BodilyHarmText,
                    opt => opt.MapFrom(source => Dictionaries.GetText(source.BodilyHarm?? BodilyHarmEnum.NotAvailable)))
            .ForMember(
                    destination => destination.EconomicLossText,
                    opt => opt.MapFrom(source => source.EconomicLoss == null ? Dictionaries.NotAvailable : source.EconomicLoss.ToString()));
            configurationExpression.CreateMap<AccidentVictimViewModel, AccidentVictim>();

            configurationExpression.CreateMap<HouseDestroyedInFire, HouseDestroyedInFireViewModel>()
                .ForMember(
                    destination => destination.HouseAddressId,
                    opt => opt.MapFrom(source => source.DestroyedHouseAddress.Id))
                .ForMember(
                    destination => destination.InitialHouseAddressId,
                    opt => opt.MapFrom(source => source.DestroyedHouseAddress.Id))
                .ForMember(
                    destination => destination.HouseAddressText,
                    opt => opt.MapFrom(source => $"г.{source.DestroyedHouseAddress.City}, ул.{source.DestroyedHouseAddress.Street}, {source.DestroyedHouseAddress.HouseNumber}"));
            configurationExpression.CreateMap<HouseDestroyedInFireViewModel, HouseDestroyedInFire>();

            configurationExpression.CreateMap<CriminalOffender, CriminalOffenderViewModel>()
                .ForMember(
                    destination => destination.OffenderName,
                    opt => opt.MapFrom(source => $"{source.Offender.FirstName} {source.Offender.LastName}"))
                .ForMember(
                    destination => destination.CitizenId,
                    opt => opt.MapFrom(source => source.Offender.Id))
                .ForMember(
                    destination => destination.InitialCitizenId,
                    opt => opt.MapFrom(source => source.Offender.Id))
                .ForMember(
                    destination => destination.AccidentId,
                    opt => opt.MapFrom(source => source.Accident.Id));
            configurationExpression.CreateMap<CriminalOffenderViewModel, CriminalOffender>();

            configurationExpression.CreateMap<CriminalOffenceArticle, CriminalOffenceArticleViewModel>()
                .ForMember(
                    destination => destination.CriminalOffenceArticleEnum,
                    opt => opt.MapFrom(source => source.OffenceArticle))
                .ForMember(
                    destination => destination.CriminalOffenceArticleText,
                    opt => opt.MapFrom(source => Dictionaries.GetText(source.OffenceArticle)));
            configurationExpression.CreateMap<CriminalOffenceArticleViewModel, CriminalOffenceArticle>()
                .ForMember(
                    destination => destination.OffenceArticle,
                    opt => opt.MapFrom(source => source.CriminalOffenceArticleEnum));

            configurationExpression.CreateMap<CitizenUser, UserViewModel>()
                .ForMember(
                    destination => destination.FullName,
                    opt => opt.MapFrom(source => source.FirstName + " " + source.LastName))
            .ForMember(
                    destination => destination.RolesString,
                    opt => opt.MapFrom(source => string.Join(", ", source.Roles.Select(r => r.Name))));
            configurationExpression.CreateMap<Adress, AddressViewModel>()
                .ForMember(
                    destination => destination.AddressText,
                    opt => opt.MapFrom(source => $"г.{source.City}, ул.{source.Street}, {source.HouseNumber}"));
            #endregion

            configurationExpression.CreateMap<Certificate, CertificateViewModel>()
                .ForMember(dest => dest.OwnerLogin, opt => opt.MapFrom(src => src.Owner.Login));

            configurationExpression.CreateMap<CertificateViewModel, Certificate>();

            configurationExpression.CreateMap<Policeman, PolicemanViewModel>()
                .ForMember(dest => dest.ProfileVM, opt => opt.MapFrom(p => p.User));

            configurationExpression.CreateMap<Violation, ViolationItemViewModel>()
                .ForMember(dest => dest.BlamedUserName, opt => opt.MapFrom(v => v.BlamedUser.FirstName + " " + v.BlamedUser.LastName))
                .ForMember(dest => dest.PolicemanName, opt => opt.MapFrom(v => v.ViewingPoliceman.User.FirstName + " " + v.ViewingPoliceman.User.LastName));

            configurationExpression.CreateMap<ViolationDeclarationViewModel, Violation>().ReverseMap();

            configurationExpression.CreateMap<CitizenUser, FoundUsersViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(u => u.FirstName + " " + u.LastName));

            configurationExpression.CreateMap<CitizenUser, UserVerificationViewModel>();

            configurationExpression.CreateMap<Violation, CriminalItemViewModel>()
                .ForMember(dest => dest.BlamingUserName, opt => opt.MapFrom(v => v.BlamingUser.FirstName + " " + v.BlamingUser.LastName))
                .ForMember(dest => dest.BlamedUserName, opt => opt.MapFrom(v => v.BlamedUser.FirstName + " " + v.BlamedUser.LastName))
                .ForMember(dest => dest.ViewingPolicemanName, opt => opt.MapFrom(v => v.ViewingPoliceman.User.FirstName + " " + v.ViewingPoliceman.User.LastName))
                .ForMember(dest => dest.PolicemanLogin, opt => opt.MapFrom(v => v.ViewingPoliceman.User.Login))
                .ForMember(dest => dest.BlamedUserAvatar, opt => opt.MapFrom(v => v.BlamedUser.AvatarUrl));

            configurationExpression.CreateMap<MedicalInsurance, MedicalInsuranceViewModel>();
            configurationExpression.CreateMap<MedicalInsuranceViewModel, MedicalInsurance>();

            configurationExpression.CreateMap<CitizenUser, ForDHLoginViewModel>();
            configurationExpression.CreateMap<ForDHLoginViewModel, CitizenUser>();

            configurationExpression.CreateMap<MedicalInsurance, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, MedicalInsurance>();

            configurationExpression.CreateMap<CitizenUser, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, CitizenUser>();

            configurationExpression.CreateMap<CitizenUser, DoctorPageViewModel>();
            configurationExpression.CreateMap<DoctorPageViewModel, CitizenUser>();

            configurationExpression.CreateMap<Role, RoleViewModel>()
                .ForMember(dest => dest.UserLogins, opt => opt.MapFrom(src => src.Users.Select(t => t.Login)));

            configurationExpression.CreateMap<RoleViewModel, Role>();

            configurationExpression.CreateMap<Transaction, TransactionViewModel>()
                .ForMember(dest => dest.SenderLogin, opt => opt.MapFrom(src => src.Sender.Login))
                .ForMember(dest => dest.RecipientLogin, opt => opt.MapFrom(src => src.Recipient.Login));

            configurationExpression.CreateMap<TransactionViewModel, Transaction>();

            configurationExpression.CreateMap<MedicineCertificate, MedicineCertificateViewModel>();
            configurationExpression.CreateMap<MedicineCertificateViewModel, MedicineCertificate>();

            configurationExpression.CreateMap<ReceptionOfPatients, ReceptionOfPatientsViewModel>();
            configurationExpression.CreateMap<ReceptionOfPatientsViewModel, ReceptionOfPatients>();

            configurationExpression.CreateMap<ReceptionOfPatients, UserPageViewModel>();
            configurationExpression.CreateMap<UserPageViewModel, ReceptionOfPatients>();

            var mapperConfiguration = new MapperConfiguration(configurationExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(s => mapper);
        }

        private void RegistrationRepository(IServiceCollection services)
        {
            services.AddScoped<CitizenUserRepository>(serviceProvider =>
            {
                var webContext = serviceProvider.GetService<WebMazeContext>();
                return new CitizenUserRepository(webContext);
            });

            services.AddScoped(s => new AdressRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new PolicemanRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new ViolationRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new HealthDepartmentRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new RecordFormRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new BusRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusStopRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusRouteRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusOrderRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new BusWorkerRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new UserTaskRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new CertificateRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new RoleRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new TransactionRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new MedicalInsuranceRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new MedicineCertificateRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new ReceptionOfPatientsRepository(s.GetService<WebMazeContext>()));

            services.AddScoped(s => new AccidentRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new VictimRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new FireDetailRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new HouseDestroyedInFireRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new CriminalOffenceArticleRepository(s.GetService<WebMazeContext>()));
            services.AddScoped(s => new CriminalOffenderRepository(s.GetService<WebMazeContext>()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Кто ты?
            app.UseAuthentication();

            // Куда у тебя есть доступ?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
