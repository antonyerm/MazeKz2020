using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace WebMaze.DbStuff.Repository.Life
{
    public class GeneratorsForLife
    {
        private CitizenUserRepository citizenUserRepository;
        private AdressRepository addressRepository;

        public GeneratorsForLife(CitizenUserRepository citizenUserRepository, AdressRepository addressRepository)
        {
            this.citizenUserRepository = citizenUserRepository;
            this.addressRepository = addressRepository;
        }
        public void GenerateCitizenUsers(int quantity)
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
                "Красинец", "Семёнов", "Новиков", "Горбачёв", "Шамрыло", "Игнатьев", "Гриневская", "Михеев", "Ермаков", "Филиппов",
            };
            var lastNamesFemale = new List<string>
            {
                "Петрова", "Пестова", "Шарапова", "Ярова", "Моисеенко", "Иващенко", "Моисеенко", "Гончар", "Кличко", "Петренко", "Шкраба",
                "Лазарева", "Егорова",  "Права", "Вишнякова", "Белоусова", "Орлова", "Плаксий", "Милославска", "Данилова", 
            };

            for (int i = 0; i < 10; i++)
            {
                string firstName;
                string lastName;
                var maleOrFemale = rnd.Next(0, 2);

                if (maleOrFemale == 0)
                {
                    firstName = firstNamesMale[rnd.Next(firstNamesMale.Count)];
                    lastName = lastNamesMale[rnd.Next(lastNamesMale.Count)];
                }
                else
                {
                    firstName = firstNamesMale[rnd.Next(firstNamesMale.Count)];
                    lastName = lastNamesMale[rnd.Next(lastNamesMale.Count)];
                }

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
                    Gender = maleOrFemale == 0 ? "муж." : "жен."
                };
                citizenUserRepository.Save(newCitizen);
            }

        }

        public void GenerateAddresses(int quantity)
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
        }
    }
}
