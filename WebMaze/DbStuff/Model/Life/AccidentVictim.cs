using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model.Life
{
    public class AccidentVictim : BaseModel
    {
        public virtual BodilyHarmEnum? BodilyHarm { get; set; }
        public virtual decimal? EconomicLoss { get; set; }

        // relations
        // the N side of 1:N
        public virtual CitizenUser Victim { get; set; }
        public virtual Accident Accident { get; set; }
    }
}
