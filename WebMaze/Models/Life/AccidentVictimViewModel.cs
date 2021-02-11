using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebMaze.DbStuff.Model.Life;
using WebMaze.Models.CustomAttribute.Life;

namespace WebMaze.Models.Life
{
    public class AccidentVictimViewModel
    {
        public long Id { get; set; }
        public long AccidentId { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите имя потерпевшего")]
        [UniqueVictim]
        public long? CitizenId { get; set; }
        public long? InitialCitizenId { get; set; }
        public string VictimName { get; set; }
        public IEnumerable<SelectListItem> CitizenList { get; set; }
        
        public BodilyHarmEnum BodilyHarm { get; set; }
        public string BodilyHarmText { get; set; }
        public IEnumerable<SelectListItem> BodilyHarmList { get; set; }
        [Display(Name = "Экономический ущерб")]
        public decimal? EconomicLoss { get; set; }
        public string EconomicLossText { get; set; }
        public AccidentCategoryEnum AccidentCategory{ get; set; }
    }
}
