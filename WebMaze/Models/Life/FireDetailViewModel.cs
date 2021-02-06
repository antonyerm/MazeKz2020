using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.Models.Life
{
    public class FireDetailViewModel
    {
        public long Id { get; set; }
        public long AccidentId { get; set; }
        public FireCauseEnum? FireCause { get; set; }
        public string FireCauseText { get; set; }
        public FireClassEnum? FireClass { get; set; }
        public string FireClassText { get; set; }
        public IEnumerable<SelectListItem> FireCauseList { get; set; }
        public IEnumerable<SelectListItem> FireClassList { get; set; }
    }
}
