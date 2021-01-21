using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.Models.Life
{
    public class AccidentVictimViewModel
    {
        public long AccidentId { get; set; }
        public string VictimName { get; set; }
        public BodilyHarmEnum? BodilyHarmCode { get; set; }
        public string BodilyHarm { get; set; }
        public string EconomicLoss { get; set; }
    }
}
