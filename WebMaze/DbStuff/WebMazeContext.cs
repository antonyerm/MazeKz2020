using Microsoft.EntityFrameworkCore;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.Medicine;
using WebMaze.DbStuff.Model.Police;
using WebMaze.DbStuff.Model.UserAccount;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff
{
    public class WebMazeContext : DbContext
    {
        public DbSet<CitizenUser> CitizenUser { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Adress> Adress { get; set; }

        public DbSet<Policeman> Policemen { get; set; }

        public DbSet<Violation> Violations { get; set; }

        public DbSet<PoliceCertificate> PoliceCertificates { get; set; }

        public DbSet<HealthDepartment> HealthDepartment { get; set; }
        public DbSet<RecordForm> RecordForms { get; set; }

        public DbSet<Bus> Bus { get; set; }

        public DbSet<BusStop> BusStop { get; set; }

        public DbSet<BusRoute> BusRoute { get; set; }

        public DbSet<BusOrder> BusOrder { get; set; }

        public DbSet<BusWorker> BusWorker { get; set; }

        public DbSet<BusRouteTime> BusRouteTime { get; set; }

        #region Life Project
        public DbSet<Accident> Accident { get; set; }
        public DbSet<AccidentVictim> AccidentVictim { get; set; }
        public DbSet<CriminalOffenceArticle> CriminalOffenceDetail { get; set; }
        public DbSet<CriminalOffender> CriminalOffender { get; set; }
        public DbSet<FireDetail> FireDetail { get; set; }
        public DbSet<HouseDestroyedInFire> HouseDestroyedInFire { get; set; }
        #endregion

        public DbSet<UserTask> UserTasks { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<MedicalInsurance> MedicalInsurances { get; set; }
        public DbSet<MedicineCertificate> MedicineCertificates { get; set; }

        public WebMazeContext(DbContextOptions dbContext) : base(dbContext) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CitizenUser>()
                .HasMany(citizen => citizen.Adresses)
                .WithOne(adress => adress.Owner);

            modelBuilder
                .Entity<CitizenUser>()
                .HasMany(p => p.Roles)
                .WithMany(p => p.Users)
                .UsingEntity(j => j.ToTable("CitizenUserRoles"));

            modelBuilder.Entity<CitizenUser>()
                .HasOne(c => c.MedicalInsurance)
                .WithOne(m => m.Owner);

            modelBuilder.Entity<CitizenUser>()
                .HasMany(citizen => citizen.RecordForms)
                .WithOne(records => records.CitizenId);

            modelBuilder.Entity<CitizenUser>()
                .HasOne(p => p.MedicineCertificate)
                .WithOne(o => o.User);

            modelBuilder.Entity<CitizenUser>()
                .HasMany(citizen => citizen.Certificates)
                .WithOne(certificate => certificate.Owner);

            #region Life Project
            // 1:N
            modelBuilder.Entity<AccidentVictim>()
                .HasOne(av => av.Accident)
                .WithMany(a => a.AccidentVictims);

            modelBuilder.Entity<CriminalOffenceArticle>()
                .HasOne(c => c.Accident)
                .WithMany(a => a.CriminalOffenceArticles);

            modelBuilder.Entity<CriminalOffender>()
                .HasOne(co => co.Offender)
                .WithMany(citizenUser => citizenUser.CriminalOffenders);

            modelBuilder.Entity<CriminalOffender>()
                .HasOne(co => co.Accident)
                .WithMany(accident => accident.CriminalOffenders);

            modelBuilder.Entity<HouseDestroyedInFire>()
                .HasOne(h => h.Accident)
                .WithMany(a => a.HousesDestroyedInFire);

            modelBuilder.Entity<HouseDestroyedInFire>()
                .HasOne(h => h.DestroyedHouseAddress)
                .WithMany(a => a.HousesDestroyedInFire);
            // 1:1
            modelBuilder.Entity<FireDetail>()
                .HasOne(fd => fd.Accident)
                .WithOne(a => a.FireDetail)
                .HasForeignKey<FireDetail>(fd => fd.AccidentId);
            // enum configuration
            modelBuilder.Entity<Accident>()
                .Property(a => a.AccidentCategory)
                .HasConversion<int>();
            modelBuilder.Entity<AccidentVictim>()
                .Property(a => a.BodilyHarm)
                .HasConversion<int>();
            modelBuilder.Entity<CriminalOffenceArticle>()
                .Property(c => c.OffenceArticle)
                .HasConversion<int>();
            modelBuilder.Entity<FireDetail>()
                .Property(f => f.FireCause)
                .HasConversion<int>();
            modelBuilder.Entity<FireDetail>()
                .Property(f => f.FireClass)
                .HasConversion<int>();
            // format configuration
            modelBuilder.Entity<AccidentVictim>()
                .Property(a => a.EconomicLoss)
                .HasColumnType("decimal(18,2");
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
