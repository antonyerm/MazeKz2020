using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Repository;

namespace WebMaze.Services
{
    public class LifeService
    {
        private CitizenUserRepository citizenUserRepository;
        private AdressRepository addressRepository;
        private RoleRepository roleRepository;
        private IHttpContextAccessor httpContextAccessor;

        public LifeService(CitizenUserRepository citizenUserRepository, 
                                 AdressRepository addressRepository,
                                 RoleRepository roleRepository,
                                 IHttpContextAccessor httpContextAccessor)
        {
            this.citizenUserRepository = citizenUserRepository;
            this.addressRepository = addressRepository;
            this.roleRepository = roleRepository;
            this.httpContextAccessor = httpContextAccessor;
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
    }
}
