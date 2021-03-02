using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.Life;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Repository.Life;
using WebMaze.Models.Account;
using WebMaze.Models.Life;

namespace WebMaze.Services
{
    public class LifeService
    {
        private IMapper mapper;
        private CitizenUserRepository citizenUserRepository;
        private AdressRepository addressRepository;
        private RoleRepository roleRepository;
        private IHttpContextAccessor httpContextAccessor;
        private AccidentRepository accidentRepository;

        public LifeService(IMapper mapper, 
                            CitizenUserRepository citizenUserRepository,
                            AccidentRepository accidentRepository,
                            AdressRepository addressRepository,
                            RoleRepository roleRepository,
                            IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.citizenUserRepository = citizenUserRepository;
            this.addressRepository = addressRepository;
            this.roleRepository = roleRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.accidentRepository = accidentRepository;
        }

        /// <summary>
        /// Creates specified number of unique users (actually creates less than that number because of limited names dictionoary)
        /// </summary>
        /// <param name="quantity">Number of new users required</param>
        /// <returns>Actual number of users created</returns>
        public int GenerateCitizenUsers(int quantity)
        {
            var rnd = new Random();
            var firstNamesMale = new List<string>
            {
                "Ярослав", "Викентий", "Фёдор", "Клаус", "Степан", "Остин", "Нестор", "Йозеф", "Филипп", "Леопольд",
                "Герман", "Йоган", "Эрик", "Лукьян", "Остап", "Шамиль", "Захар", "Михаил",
            };
            var firstNamesFemale = new List<string>
            {
                "Харитина", "Надежда", "Татьяна", "Клара", "Богдана", "Ульяна", "Зинаида", "Белла", "Рада", "Надежда",
                "Мария", "Генриетта", "Бронислава", "Изольда", "Шарлота", "Эмилия", "Алла", "Цара", "Зоя", "Шарлота",
            };
            var lastNamesMale = new List<string>
            {
                "Котов", "Рыбаков", "Горшков", "Меркушев", "Фокин", "Матвеев", "Цушко", "Васильев", "Афанасьев", "Барановский",
                "Красинец", "Семёнов", "Новиков", "Горбачёв", "Шамрыло", "Игнатьев", "Гриневский", "Михеев", "Ермаков", "Филиппов",
            };
            var lastNamesFemale = new List<string>
            {
                "Петрова", "Пестова", "Шарапова", "Ярова", "Моисеенко", "Иващенко", "Моисеенко", "Гончар", "Кличко", "Петренко", "Шкраба",
                "Лазарева", "Егорова",  "Права", "Вишнякова", "Белоусова", "Орлова", "Плаксий", "Милославска", "Данилова",
            };
            var ListOfFullNames = citizenUserRepository.GetAll()
                .Select(user => user.FirstName + " " + user.LastName).ToList();
            var ListOfRoles = roleRepository.GetAll();
            
            // количество попыток найти уникальное сочетание имя+фамилия
            var attemptNumber = 0;
            // количество успешных попыток найти уникальное сочетание имя+фамилия
            var successfulAttemptNumber = 0;


            do
            {
                string firstName;
                string lastName;
                var isMale = rnd.Next(0, 2);


                if (isMale == 1)
                {
                    firstName = firstNamesMale[rnd.Next(firstNamesMale.Count)];
                    lastName = lastNamesMale[rnd.Next(lastNamesMale.Count)];
                }
                else
                {
                    firstName = firstNamesMale[rnd.Next(firstNamesMale.Count)];
                    lastName = lastNamesMale[rnd.Next(lastNamesMale.Count)];
                }

                var FullName = firstName + " " + lastName;
                if (!ListOfFullNames.Any(s => s == FullName))
                {
                    // имя-фамилия уникально, сохраняем
                    ListOfFullNames.Add(FullName);
                    var startBirthdate = new DateTime(1950, 1, 1);
                    var range = (new DateTime(2010, 1, 1) - startBirthdate).Days;
                    var birthdate = startBirthdate.AddDays(rnd.Next(range));

                    var newCitizen = new CitizenUser
                    {
                        Id = 0,
                        Login = firstName + "_" + lastName,
                        Password = "123",
                        FirstName = firstName,
                        LastName = lastName,
                        BirthDate = birthdate,
                        Gender = isMale == 1 ? Gender.Male : Gender.Female,
                        Roles = new List<Role>(),
                    };
                    newCitizen.Roles.Add(ListOfRoles[rnd.Next(ListOfRoles.Count)]);
                    citizenUserRepository.Save(newCitizen);
                    successfulAttemptNumber++;
                }
                // имя-фамилия не уникально, ищем другие
                attemptNumber++;
            }
            while (attemptNumber < quantity);
            return successfulAttemptNumber;
        }

        /// <summary>
        /// Creates specified number of addresses (houses)
        /// </summary>
        /// <param name="quantity">Requested number of addresses</param>
        /// <returns>Actual number of houses created</returns>
        public int GenerateAddresses(int quantity)
        {
            var rnd = new Random();

            var cities = new List<string>
            {
                "Алматы", "Нур-Султан", "Шымкент", "Караганды", "Актобе", "Тараз", "Павлодар", "Усть-Каменогорск", "Семей", "Уральск",
                "Костанай", "Кызылорда", "Петропавловск", "Атырау", "Актау", "Темиртау", "Туркестан", "Кокшетау", "Талдыкорган", "Экибастуз",
                "Рудный", "Жанаозен", "Жезказган", "Байконыр", "Балхаш", "Кентау", "Каскелен", "Сатпаев", "Кулсары", "Риддер",
            };
            var streets = new List<string>
            {
                "Абая", "Басенова", "Калдаякова", "Гагарина", "Гоголя", "Аль-Фараби", "Джамбула", "Наурызбай батыра", "Емелева",
                "Жарокова", "Мауленова", "Кунаева", "Клочкова", "Абылай хана", "Толе би", "Байтурсынова", "Курмангазы", "Маметовой",
                "Масанчи", "Мате Залки", "Желтоксан", "Муканова", "Муратбаева", "Мынбаева", "Навои", "Айтеке би", "Панфилова",
                "Макатаева", "Пушкина", "Сатпаева", "Сейфуллина", "Тулебаева", "Туркебаева", "Фурманова", "Шевченко", "Шагабутдинова",
                "Шолохова", "Абдуллиных",
            };

            for (int i = 0; i < quantity; i++)
            {
                var newAddress = new Adress
                {
                    Id = 0,
                    City = cities[rnd.Next(cities.Count)],
                    Street = streets[rnd.Next(streets.Count)],
                    HouseNumber = rnd.Next(99) + 1
                };
                addressRepository.Save(newAddress);
            }

            return quantity;
        }

        /// <summary>
        /// Takes current user registered in Http.User
        /// </summary>
        /// <returns>CitizenUser object</returns>
        public CitizenUser GetCurrentUser()
        {
            var idStr = httpContextAccessor.HttpContext.
                User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = int.Parse(idStr);
            var citizen = citizenUserRepository.Get(id);
            return citizen;
        }

        /// <summary>
        /// Prepares Accident Details View Model for Accident Details, EditFire and Edit Criminal Offence view
        /// </summary>
        /// <param name="id">Accident Id</param>
        /// <returns>Accident Details View Model instance</returns>
        public AccidentDetailsViewModel GetAccidentDetailsViewModel(long id)
        {
            var accidentFromDb = accidentRepository.Get(id);
            var accidentDetailsViewModel = mapper.Map<AccidentDetailsViewModel>(accidentFromDb);

            // victims list
            // no items in the list = there were no victims (not "no info available")
            var victimsFromDb = accidentFromDb.AccidentVictims;
            var accidentVictimsViewModel = new List<AccidentVictimViewModel>();
            if (victimsFromDb.Count != 0)
            {
                foreach (var victim in victimsFromDb)
                {
                    accidentVictimsViewModel.Add(mapper.Map<AccidentVictimViewModel>(victim));
                }
            }
            accidentDetailsViewModel.AccidentVictimsViewModel = accidentVictimsViewModel;

            // address
            var accidentAddressFromDb = accidentFromDb.AccidentAddress;
            var addressViewModel = mapper.Map<AdressViewModel>(accidentAddressFromDb);
            accidentDetailsViewModel.AccidentAddressViewModel = addressViewModel;

            accidentDetailsViewModel.AccidentAddressText = addressViewModel == null ? Dictionaries.NotAvailable : $"г.{addressViewModel.City}, ул.{addressViewModel.Street}, {addressViewModel.HouseNumber}";

            // Fire details
            var fireDetailFromDb = accidentFromDb.FireDetail;
            var fireDetailViewModel = mapper.Map<FireDetailViewModel>(fireDetailFromDb);
            if (fireDetailViewModel == null)
            {
                fireDetailViewModel = new FireDetailViewModel()
                {
                    AccidentId = accidentFromDb.Id,
                    FireCause = FireCauseEnum.NotAvailable,
                    FireCauseText = Dictionaries.GetText<FireCauseEnum>(FireCauseEnum.NotAvailable),
                    FireClass = FireClassEnum.NotAvailable,
                    FireClassText = Dictionaries.GetText<FireClassEnum>(FireClassEnum.NotAvailable),
                };
            }
            accidentDetailsViewModel.FireDetailViewModel = fireDetailViewModel;

            // Houses Destroyed in fire list
            // no items in the list = there were no destroyed houes (not "no info available")
            var housesDestroyedInFireFromDb = accidentFromDb.HousesDestroyedInFire;
            var housesDestroyedInFireViewModel = new List<HouseDestroyedInFireViewModel>();
            if (housesDestroyedInFireFromDb.Count != 0)
            {
                foreach (var house in housesDestroyedInFireFromDb)
                {
                    housesDestroyedInFireViewModel.Add(mapper.Map<HouseDestroyedInFireViewModel>(house));
                }
            }
            accidentDetailsViewModel.HousesDestroyedInFireViewModel = housesDestroyedInFireViewModel;

            // Criminal Offence
            // Criminal offence articles
            var criminalArticlesFromDb = accidentFromDb.CriminalOffenceArticles;
            var criminalOffenceArticlesViewModel = new List<CriminalOffenceArticleViewModel>();
            if (criminalArticlesFromDb.Count != 0)
            {
                foreach (var article in criminalArticlesFromDb)
                {
                    var articleViewModel = mapper.Map<CriminalOffenceArticleViewModel>(article);
                    criminalOffenceArticlesViewModel.Add(articleViewModel);
                }
            }
            accidentDetailsViewModel.CriminalOffenceArticlesViewModel = criminalOffenceArticlesViewModel;

            // Criminal offenders
            var offendersFromDb = accidentFromDb.CriminalOffenders;
            var accidentOffendersViewModel = new List<CriminalOffenderViewModel>();
            if (offendersFromDb.Count != 0)
            {
                foreach (var offender in offendersFromDb)
                {
                    accidentOffendersViewModel.Add(mapper.Map<CriminalOffenderViewModel>(offender));
                }
            }
            accidentDetailsViewModel.CriminalOffendersViewModel = accidentOffendersViewModel;

            return accidentDetailsViewModel;
        }

        /// <summary>
        /// Prepares Select list for dropdown of any Enum properties
        /// </summary>
        /// <typeparam name="T">Type of Enum propertie</typeparam>
        /// <returns>Select List for dropdown filled with Enum text values</returns>
        public SelectList GetSelectListFromEnum<T>() where T : Enum
        {
            var listOfValues = new List<SelectListItem>();
            foreach (var valueAsObject in Enum.GetValues(typeof(T)))
            {
                listOfValues.Add(new SelectListItem
                {
                    Value = ((int)valueAsObject).ToString(),
                    Text = Dictionaries.GetText((T)valueAsObject)
                });
            };
            var selectList = new SelectList(listOfValues, "Value", "Text");
            return selectList;
        }

        /// <summary>
        /// Prepares Select List for dropdown of addresses
        /// </summary>
        /// <returns>Select List for dropdown filled with addresses as text</returns>
        public SelectList GetSelectListOfAddressesFromDb()
        {
            // TODO: take only a few records from address table
            var addressListFromDb = this.addressRepository.GetAll().
                Select(a => new { Id = a.Id, Address = $"{a.City}, ул.{a.Street}, {a.HouseNumber}" });
            var selectList = new SelectList(addressListFromDb, "Id", "Address");
            return selectList;
        }

        /// <summary>
        /// Prepares Select List for dropdown of citizens
        /// </summary>
        /// <returns>Select List for dropdown filled with users as text</returns>
        public SelectList GetSelectListOfCitizensFromDb()
        {
            // TODO: take only a few records from citizens table
            var citizensListFromDb = this.citizenUserRepository.GetAll().
                Select(c => new { Id = c.Id, Name = $"{c.FirstName} {c.LastName}" });

            //var citizensListFromDb = this.citizenUserRepository.GetAll();
            var selectList = new SelectList(citizensListFromDb, "Id", "Name");
            return selectList;
        }
    }
}
