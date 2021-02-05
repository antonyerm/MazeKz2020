using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.Models.Life
{
    public class AccidentVictimViewModel
    {
        public long Id { get; set; }
        public long AccidentId { get; set; }
        public long CitizenId { get; set; }
        public string VictimName { get; set; }
        public IEnumerable<SelectListItem> CitizenList { get; set; }
        public BodilyHarmEnum? BodilyHarm { get; set; }
        public string BodilyHarmText { get; set; }
        public IEnumerable<SelectListItem> BodilyHarmList { get; set; }
        
        public decimal EconomicLoss { get; set; }
        public AccidentCategoryEnum AccidentCategory{ get; set; }
    }
}
