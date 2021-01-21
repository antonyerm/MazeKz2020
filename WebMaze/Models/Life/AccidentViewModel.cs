using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models.Life
{
    public class AccidentViewModel
    {
        public DateTime AccidentDate { get; set; }
        public string AccidentAddress { get; set; }
        public string AccidentCategory { get; set; }
        public string AccidentDescription { get; set; }
    }
}
