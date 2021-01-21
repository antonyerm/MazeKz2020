using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model.Life
{
    public class Dictionaries
    {
        public static Dictionaries singleton;
        
        public Dictionary<AccidentCategoryEnum, string> AccidentCategory = new Dictionary<AccidentCategoryEnum, string>
        {
            [AccidentCategoryEnum.NotDefined] = "Неизвестно",
            [AccidentCategoryEnum.Fire] = "Пожар",
            [AccidentCategoryEnum.CriminalOffence] = "Преступление",
        };

        public Dictionary<BodilyHarmEnum, string> BodilyHarm = new Dictionary<BodilyHarmEnum, string>
        {
            [BodilyHarmEnum.BodilyInjury] = "Телесные повреждения",
            [BodilyHarmEnum.SeriousBodilyInjury] = "Тяжкие телесные повреждения",
            [BodilyHarmEnum.Death] = "Смерть",
        };

        public Dictionary<CriminalCodeEnum, string> CriminalCode = new Dictionary<CriminalCodeEnum, string>
        {
            [CriminalCodeEnum.OffencesAgainstPublicOrder] = "Нарушение общественного порядка",
            [CriminalCodeEnum.Terrorism] = "Терроризм",
            [CriminalCodeEnum.FirearmsAndOtherWeapons] = "Применение огнестрельного и другого оружия",
            [CriminalCodeEnum.OffencesAgainstTheAdministrationOfLawAndJustice] = "Нарушение при отправлении правосудия",
            [CriminalCodeEnum.SexualOffencesPublicMoralsAndDisorderlyConduct] = "Преступление на сексуальной почве",
            [CriminalCodeEnum.InvasionOfPrivacy] = "Вторжение в личную жизнь",
            [CriminalCodeEnum.DisorderlyHousesGamingAndBetting] = "Нарушение в азартных играх",
            [CriminalCodeEnum.OffencesAgainstThePersonAndReputation] = "Преступление против личности",
            [CriminalCodeEnum.OffencesRelatingToConveyances] = "Преступления на транспорте",
            [CriminalCodeEnum.OffencesAgainstRightsOfProperty] = "Посягательство на имущество",
            [CriminalCodeEnum.FraudulentTransactionsRelatingToContractsAndTrade] = "Мошенническая сделка",
            [CriminalCodeEnum.WilfulAndForbiddenActsInRespectOfCertainProperty] = "Злонамеренное действие в отношении собственности",
            [CriminalCodeEnum.OffencesRelatingToCurrency] = "Валютное мошенничество",
        };

        public Dictionary<FireCauseEnum, string> FireCause = new Dictionary<FireCauseEnum, string>
        {

            [FireCauseEnum.unknown] = "Неизвестно",
            [FireCauseEnum.lightning] = "Молния",
            [FireCauseEnum.volcanism] = "Вулкан",
            [FireCauseEnum.gasEmission] = "Выход природного газа",
            [FireCauseEnum.electicalPower] = "Линии электропередач",
            [FireCauseEnum.railroads] = "Железная дорога",
            [FireCauseEnum.vehicles] = "Транспортные средства",
            [FireCauseEnum.works] = "Проведение работ",
            [FireCauseEnum.weapons] = "Оружие",
            [FireCauseEnum.selfIgnition] = "Самовозгорание",
            [FireCauseEnum.vegetationManagement] = "Сельское хозяйство",
            [FireCauseEnum.wasteManagement] = "Управление отходами",
            [FireCauseEnum.recreation] = "Отдых",
            [FireCauseEnum.fireworks] = "Пиротехника",
            [FireCauseEnum.cigarettes] = "Сигареты",
            [FireCauseEnum.hotAshes] = "Горячая зола",
            [FireCauseEnum.interestProfit] = "Умышленный поджиг",
            [FireCauseEnum.conflictRevenge] = "Месть в конфликте",
            [FireCauseEnum.vandalism] = "Вандализм",
            [FireCauseEnum.excitement] = "Интерес",
            [FireCauseEnum.crimeConcealment] = "Сокрытие преступления",
            [FireCauseEnum.extremist] = "Экстремизм",
            [FireCauseEnum.mentalIllnes] = "Умственная недостаточность",
            [FireCauseEnum.children] = "Дети",
            [FireCauseEnum.rekindle] = "Повторное возгорание",
        };

        public Dictionary<FireClassEnum, string> FireClass = new Dictionary<FireClassEnum, string>
        {
            [FireClassEnum.fireClassA] = "Класс A. Обычные горючие материалы",
            [FireClassEnum.fireClassB] = "Класс B. Воспламеняющиеся жидкости (топливо)",
            [FireClassEnum.fireClassC] = "Класс C. Оборудование под напряжением",
            [FireClassEnum.fireClassD] = "Класс D. Воспламеняющиеся металлы",
            [FireClassEnum.fireClassK] = "Класс K. Кухонные масла",
        };

        public Dictionaries()
        {
        }

        public static Dictionaries GetText()
        {
            if (singleton == null)
            {
                singleton = new Dictionaries();
            }

            return singleton;
        }

    }
}
