﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMaze.DbStuff;

namespace WebMaze.Migrations
{
    [DbContext(typeof(WebMazeContext))]
    [Migration("20210120113800_Life-Initial")]
    partial class LifeInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("WebMaze.DbStuff.Model.Adress", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Adress");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Bus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("BusRouteId")
                        .HasColumnType("bigint");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationPlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("WorkerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BusRouteId");

                    b.ToTable("Bus");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.BusRoute", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusRoute");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.BusStop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusStop");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.CitizenUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDead")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CitizenUser");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.HealthDepartment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HealthDepartment");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.Accident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("AccidentAddressId")
                        .HasColumnType("bigint");

                    b.Property<int>("AccidentCategory")
                        .HasColumnType("int");

                    b.Property<DateTime>("AccidentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccidentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccidentAddressId");

                    b.ToTable("Accident");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.AccidentVictim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("AccidentId")
                        .HasColumnType("bigint");

                    b.Property<int?>("BodilyHarm")
                        .HasColumnType("int");

                    b.Property<decimal?>("EconomicLoss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("VictimId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccidentId");

                    b.HasIndex("VictimId");

                    b.ToTable("AccidentVictim");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.CriminalOffenceArticle", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("AccidentId")
                        .HasColumnType("bigint");

                    b.Property<int>("OffenceArticle")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccidentId");

                    b.ToTable("CriminalOffenceDetail");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.CriminalOffender", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("AccidentId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OffenderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Verdict")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccidentId");

                    b.HasIndex("OffenderId");

                    b.ToTable("CriminalOffender");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.FireDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("AccidentId")
                        .HasColumnType("bigint");

                    b.Property<int?>("FireCause")
                        .HasColumnType("int");

                    b.Property<int?>("FireClass")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccidentId")
                        .IsUnique();

                    b.ToTable("FireDetail");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.HouseDestroyedInFire", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("AccidentId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DestroyedHouseAddressId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccidentId");

                    b.HasIndex("DestroyedHouseAddressId");

                    b.ToTable("HouseDestroyedInFire");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.Policeman", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Policemen");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.Violation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("BlamingPolicemanId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("TypeOfViolationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BlamingPolicemanId");

                    b.HasIndex("TypeOfViolationId");

                    b.HasIndex("UserId");

                    b.ToTable("Violations");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.ViolationType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Article")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Penalty")
                        .HasColumnType("money");

                    b.Property<string>("Punishment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TermOfPunishment")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TypesOfViolation");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.UserTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserTasks");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Adress", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "Owner")
                        .WithMany("Adresses")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Bus", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.BusRoute", "BusRoute")
                        .WithMany()
                        .HasForeignKey("BusRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusRoute");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.Accident", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Adress", "AccidentAddress")
                        .WithMany()
                        .HasForeignKey("AccidentAddressId");

                    b.Navigation("AccidentAddress");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.AccidentVictim", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Life.Accident", "Accident")
                        .WithMany("AccidentVictims")
                        .HasForeignKey("AccidentId");

                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "Victim")
                        .WithMany("AccidentVictims")
                        .HasForeignKey("VictimId");

                    b.Navigation("Accident");

                    b.Navigation("Victim");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.CriminalOffenceArticle", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Life.Accident", "Accident")
                        .WithMany("CriminalOffenceArticles")
                        .HasForeignKey("AccidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accident");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.CriminalOffender", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Life.Accident", "Accident")
                        .WithMany("CriminalOffenders")
                        .HasForeignKey("AccidentId");

                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "Offender")
                        .WithMany("CriminalOffenders")
                        .HasForeignKey("OffenderId");

                    b.Navigation("Accident");

                    b.Navigation("Offender");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.FireDetail", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Life.Accident", "Accident")
                        .WithOne("FireDetail")
                        .HasForeignKey("WebMaze.DbStuff.Model.Life.FireDetail", "AccidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accident");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.HouseDestroyedInFire", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Life.Accident", "Accident")
                        .WithMany("HousesDestroyedInFire")
                        .HasForeignKey("AccidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMaze.DbStuff.Model.Adress", "DestroyedHouseAddress")
                        .WithMany("HousesDestroyedInFire")
                        .HasForeignKey("DestroyedHouseAddressId");

                    b.Navigation("Accident");

                    b.Navigation("DestroyedHouseAddress");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.Policeman", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.Violation", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Police.Policeman", "BlamingPoliceman")
                        .WithMany()
                        .HasForeignKey("BlamingPolicemanId");

                    b.HasOne("WebMaze.DbStuff.Model.Police.ViolationType", "TypeOfViolation")
                        .WithMany("Violations")
                        .HasForeignKey("TypeOfViolationId");

                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("BlamingPoliceman");

                    b.Navigation("TypeOfViolation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Adress", b =>
                {
                    b.Navigation("HousesDestroyedInFire");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.CitizenUser", b =>
                {
                    b.Navigation("AccidentVictims");

                    b.Navigation("Adresses");

                    b.Navigation("CriminalOffenders");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Life.Accident", b =>
                {
                    b.Navigation("AccidentVictims");

                    b.Navigation("CriminalOffenceArticles");

                    b.Navigation("CriminalOffenders");

                    b.Navigation("FireDetail");

                    b.Navigation("HousesDestroyedInFire");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.ViolationType", b =>
                {
                    b.Navigation("Violations");
                });
#pragma warning restore 612, 618
        }
    }
}
