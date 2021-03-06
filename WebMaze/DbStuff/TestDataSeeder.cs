﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.UserAccount;
using WebMaze.DbStuff.Repository;

namespace WebMaze.DbStuff
{
    public class TestDataSeeder
    {
        private CitizenUserRepository citizenUserRepository;
        private RoleRepository roleRepository;

        public TestDataSeeder(IServiceScope scope)
        {
            citizenUserRepository = scope.ServiceProvider.GetService<CitizenUserRepository>();
            roleRepository = scope.ServiceProvider.GetService<RoleRepository>();

            if (citizenUserRepository == null || roleRepository == null)
            {
                throw new Exception("Cannot get services from ServiceProvider.");
            }
        }

        public void SeedData()
        {
            AddDoctors();
            AddPolicemen();
            AddRegularUsers();

            AddCertificates();
        }

        private void AddDoctors()
        {
            var doctors = new List<CitizenUser>()
            {
                new CitizenUser
                {
                    Login = "Tsoi",
                    Password = "123",
                    Balance = 300000,
                    RegistrationDate = new DateTime(2020, 10, 28),
                    LastLoginDate = new DateTime(2020, 10, 28),
                    FirstName = "Alexey",
                    LastName = "Tsoi",
                    Gender = Gender.Male,
                    Email = "AlexeyTsoi@example.com",
                    PhoneNumber = "3333333333",
                    BirthDate = new DateTime(1977, 4, 2)
                }
            };
            AddIfNotExistUsersWithRole(doctors, "Doctor");
        }

        private void AddPolicemen()
        {
            var policemen = new List<CitizenUser>()
            {
                new CitizenUser
                {
                    Login = "Chuck",
                    Password = "123",
                    Balance = 6000000,
                    RegistrationDate = new DateTime(2020, 12, 30),
                    LastLoginDate = new DateTime(2020, 12, 30),
                    FirstName = "Chuck",
                    LastName = "Norris",
                    Gender = Gender.Male,
                    Email = "ChuckNorris@example.com",
                    PhoneNumber = "4444444444",
                    BirthDate = new DateTime(1940, 3, 10)
                }
            };
            AddIfNotExistUsersWithRole(policemen, "Policeman");
        }

        private void AddRegularUsers()
        {
            var regularUsers = new List<CitizenUser>()
            {
                new CitizenUser
                {
                    Login = "Ivan",
                    Password = "123",
                    Balance = 1000,
                    RegistrationDate = new DateTime(2021, 1, 12),
                    LastLoginDate = new DateTime(2021, 1, 12),
                    FirstName = "Ivan",
                    LastName = "Sokolov",
                    Gender = Gender.Male,
                    Email = "IvanSokolov@example.com",
                    PhoneNumber = "5555555555",
                    BirthDate = new DateTime(1980, 5, 17)
                },
                new CitizenUser
                {
                    Login = "Anastasia",
                    Password = "123",
                    Balance = 30000,
                    RegistrationDate = new DateTime(2021, 1, 15),
                    LastLoginDate = new DateTime(2021, 1, 15),
                    FirstName = "Anastasia",
                    LastName = "Kuznecova",
                    Gender = Gender.Female,
                    Email = "AnastasiaKuznecova@example.com",
                    PhoneNumber = "66666666",
                    BirthDate = new DateTime(1990, 11, 22)
                },
                new CitizenUser
                {
                    Login = "Arnold",
                    Password = "123",
                    Balance = 0,
                    RegistrationDate = new DateTime(2021, 1, 16),
                    LastLoginDate = new DateTime(2021, 1, 16),
                    FirstName = "Arnold",
                    LastName = "Goldenberg",
                    Gender = Gender.Male,
                    Email = "ArnoldGoldenberg@example.com",
                    PhoneNumber = "77777777",
                    BirthDate = new DateTime(1977, 5, 10)
                },
                new CitizenUser
                {
                    Login = "Aigerim",
                    Password = "123",
                    Balance = 8000,
                    RegistrationDate = new DateTime(2021, 1, 17),
                    LastLoginDate = new DateTime(2021, 1, 17),
                    FirstName = "Aigerim",
                    LastName = "Alieva",
                    Gender = Gender.Female,
                    Email = "AigerimAlieva@example.com",
                    PhoneNumber = "8888888888",
                    BirthDate = new DateTime(1983, 8, 3)
                },
                new CitizenUser
                {
                    Login = "Dias",
                    Password = "123",
                    Balance = 15000,
                    RegistrationDate = new DateTime(2021, 1, 19),
                    LastLoginDate = new DateTime(2021, 1, 19),
                    FirstName = "Dias",
                    LastName = "Karimov",
                    Gender = Gender.Male,
                    Email = "DiasKarimov@example.com",
                    PhoneNumber = "9999999999",
                    BirthDate = new DateTime(2005, 10, 5)
                }
            };

            AddIfNotExistUsersWithRole(regularUsers);
        }

        private void AddIfNotExistUsersWithRole(List<CitizenUser> users, string roleName = null)
        {
            foreach (var user in users.Where(u => !citizenUserRepository.UserExists(u.Login)))
            {
                if (roleName != null)
                {
                    var role = roleRepository.GetRoleByName(roleName);
                    user.Roles.Add(role);
                }

                citizenUserRepository.Save(user);
            }
        }

        private void AddCertificates()
        {
            var allCitizens = citizenUserRepository.GetUsersAsQueryable();

            // Ensure that all citizens have a birth certificate.
            AddIfNotExistCertificateToCitizens(allCitizens, "Birth Certificate");

            // Ensure that 5 citizens have a diploma.
            var citizenLoginsWithDiploma = new List<string> { "Bill", "Musk", "Stroustrup", "Tsoi", "Chuck" };
            var citizenWithDiploma = citizenUserRepository.GetUsersByLogins(citizenLoginsWithDiploma);
            AddIfNotExistCertificateToCitizens(citizenWithDiploma, "Diploma of Higher Education");

            // Ensure that citizens have a policeman certificate.
            var citizenLoginsWithPoliceCertificate = new List<string> { "Chuck" };
            var policemen = citizenUserRepository.GetUsersByLogins(citizenLoginsWithPoliceCertificate);
            AddIfNotExistCertificateToCitizens(policemen, "Policeman Certificate");

            // Ensure that citizens have a doctor certificate.
            var citizenLoginsWithDoctorCertificate = new List<string> { "Tsoi" };
            var doctors = citizenUserRepository.GetUsersByLogins(citizenLoginsWithDoctorCertificate);
            AddIfNotExistCertificateToCitizens(doctors, "Doctor Certificate");
        }

        private void AddIfNotExistCertificateToCitizens(IQueryable<CitizenUser> citizens, string certificateName)
        {
            foreach (var citizen in citizens.Where(user => user.Certificates.All(certificate => certificate.Name != certificateName)).ToList())
            {
                var certificate = GenerateCertificate(certificateName, citizen);
                citizen.Certificates.Add(certificate);
                citizenUserRepository.Save(citizen);
            }
        }

        private Certificate GenerateCertificate(string certificateName, CitizenUser owner)
        {
            var certificate = new Certificate
            {
                Name = certificateName,
                Owner = owner
            };

            switch (certificateName)
            {
                case "Diploma of Higher Education":
                    certificate.Description =
                        "The document certifies that the person completed a course of study in a university";
                    certificate.IssuedBy = "University";
                    certificate.IssueDate = owner.BirthDate + TimeSpan.FromDays(22 * 365);
                    certificate.ExpiryDate = DateTime.MaxValue;
                    break;
                case "Birth Certificate":
                    certificate.Description = "The certificate documents the birth of the person";
                    certificate.IssuedBy = "Hospital";
                    certificate.IssueDate = owner.BirthDate;
                    certificate.ExpiryDate = DateTime.MaxValue;
                    break;
                case "Policeman Certificate":
                    certificate.Description = "The document assure qualification to work as a policeman";
                    certificate.IssuedBy = "Police";
                    certificate.IssueDate = new DateTime(2021, 1, 28);
                    certificate.ExpiryDate = new DateTime(2022, 1, 28);
                    break;
                case "Doctor Certificate":
                    certificate.Description = "The document assure qualification to work as a doctor";
                    certificate.IssuedBy = "Health Department";
                    certificate.IssueDate = new DateTime(2020, 5, 3);
                    certificate.ExpiryDate = new DateTime(2021, 5, 3);
                    break;
            }

            return certificate;
        }
    }
}

