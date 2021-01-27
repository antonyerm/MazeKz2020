using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models.Life
{
    public class AccidentViewModel
    {
        public DateTime AccidentDate { get; set; }
        public long? SelectedAccidentAddress { get; set; }
        public IEnumerable<SelectListItem> AccidentAddressList { get; set; }
        public int? SelectedAccidentCategory { get; set; }
        public IEnumerable<SelectListItem> AccidentCategoryList { get; set; }
        public string AccidentDescription { get; set; }
    }
}
