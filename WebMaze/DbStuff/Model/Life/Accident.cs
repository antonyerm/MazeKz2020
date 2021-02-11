using System;
using System.Collections.Generic;

namespace WebMaze.DbStuff.Model.Life
{
    public class Accident : BaseModel
    {
        public virtual Adress AccidentAddress { get; set; }
        public virtual DateTime AccidentDate { get; set; }
        public virtual string AccidentDescription { get; set; }

        public virtual AccidentCategoryEnum AccidentCategory { get; set; }

        // relations
        // The principal side of 1:0..1 relations
        public virtual FireDetail FireDetail { get; set; }
        // The 1 side of 1:0..N relations
        public virtual ICollection<HouseDestroyedInFire> HousesDestroyedInFire { get; set; }
        public virtual ICollection<CriminalOffenceArticle> CriminalOffenceArticles { get; set; }
        public virtual ICollection<AccidentVictim> AccidentVictims { get; set; }
        public virtual ICollection<CriminalOffender> CriminalOffenders { get; set; }
    }
}
