using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.Models.Life
{
    public class FireViewModel
    {
        public long AccidentId { get; set; }
        public FireCauseEnum? FireCauseCode { get; set; }
        public string FireCause { get; set; }
        public FireClassEnum? FireClassCode { get; set; }
        public string FireClass{ get; set; }
        public List<string> DestroyedHouseAddress { get; set; }
        public List<AccidentVictim> Victim { get; set; }
    }
}
