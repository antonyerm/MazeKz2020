using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model.Life
{
    public class CriminalOffender : BaseModel
    {
        public virtual string Verdict { get; set; }

        // relations
        // the N side of 1:N
        public virtual CitizenUser Offender { get; set; }
        public virtual Accident Accident { get; set; }
    }
}
