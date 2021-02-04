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
    [Migration("20210204082221_AddedPolicemanCommentaryField")]
    partial class AddedPolicemanCommentaryField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CitizenUserRole", b =>
                {
                    b.Property<long>("RolesId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("CitizenUserRoles");
                });

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

                    b.Property<string>("BusModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("BusRouteId")
                        .HasColumnType("bigint");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationPlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("WorkerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BusRouteId");

                    b.HasIndex("WorkerId")
                        .IsUnique()
                        .HasFilter("[WorkerId] IS NOT NULL");

                    b.ToTable("Bus");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.BusOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TargetedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BusOrder");
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

            modelBuilder.Entity("WebMaze.DbStuff.Model.BusRouteTime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("EndingPoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.Property<string>("StartingPoint")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusRouteTime");
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

            modelBuilder.Entity("WebMaze.DbStuff.Model.BusWorker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("License")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusWorker");
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

                    b.Property<bool>("HaveChildren")
                        .HasColumnType("bit");

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

                    b.Property<bool>("Marriage")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("WebMaze.DbStuff.Model.Medicine.MedicalInsurance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<decimal>("Coast")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndPeriod")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HaveChildren")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMaried")
                        .HasColumnType("bit");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartPeriod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("MedicalInsurances");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Medicine.MedicineCertificate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateExpiration")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("MedicineCertificates");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Medicine.RecordForm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("CitizenIdId")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CitizenIdId");

                    b.ToTable("RecordForms");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.Policeman", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("Rank")
                        .HasColumnType("int");

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

                    b.Property<long?>("BlamedUserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("BlamingUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ConfirmDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Explanation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OffenseType")
                        .HasColumnType("int");

                    b.Property<decimal?>("Penalty")
                        .HasColumnType("money");

                    b.Property<string>("PolicemanCommentary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TermOfPunishment")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ViewingPolicemanId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BlamedUserId");

                    b.HasIndex("BlamingUserId");

                    b.HasIndex("ViewingPolicemanId");

                    b.ToTable("Violations");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.UserAccount.Certificate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Certificates");
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

            modelBuilder.Entity("CitizenUserRole", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .WithMany("Buses")
                        .HasForeignKey("BusRouteId");

                    b.HasOne("WebMaze.DbStuff.Model.BusWorker", "Worker")
                        .WithOne("Bus")
                        .HasForeignKey("WebMaze.DbStuff.Model.Bus", "WorkerId");

                    b.Navigation("BusRoute");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Medicine.MedicalInsurance", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "Owner")
                        .WithOne("MedicalInsurance")
                        .HasForeignKey("WebMaze.DbStuff.Model.Medicine.MedicalInsurance", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Medicine.MedicineCertificate", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "User")
                        .WithOne("MedicineCertificate")
                        .HasForeignKey("WebMaze.DbStuff.Model.Medicine.MedicineCertificate", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Medicine.RecordForm", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "CitizenId")
                        .WithMany("RecordForms")
                        .HasForeignKey("CitizenIdId");

                    b.Navigation("CitizenId");
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
                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "BlamedUser")
                        .WithMany()
                        .HasForeignKey("BlamedUserId");

                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "BlamingUser")
                        .WithMany()
                        .HasForeignKey("BlamingUserId");

                    b.HasOne("WebMaze.DbStuff.Model.Police.Policeman", "ViewingPoliceman")
                        .WithMany("Violations")
                        .HasForeignKey("ViewingPolicemanId");

                    b.Navigation("BlamedUser");

                    b.Navigation("BlamingUser");

                    b.Navigation("ViewingPoliceman");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.UserAccount.Certificate", b =>
                {
                    b.HasOne("WebMaze.DbStuff.Model.CitizenUser", "Owner")
                        .WithMany("Certificates")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.BusRoute", b =>
                {
                    b.Navigation("Buses");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.BusWorker", b =>
                {
                    b.Navigation("Bus");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.CitizenUser", b =>
                {
                    b.Navigation("Adresses");

                    b.Navigation("Certificates");

                    b.Navigation("MedicalInsurance");

                    b.Navigation("MedicineCertificate");

                    b.Navigation("RecordForms");
                });

            modelBuilder.Entity("WebMaze.DbStuff.Model.Police.Policeman", b =>
                {
                    b.Navigation("Violations");
                });
#pragma warning restore 612, 618
        }
    }
}
