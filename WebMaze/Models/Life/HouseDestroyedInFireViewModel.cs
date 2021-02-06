using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;
using WebMaze.Models.Account;

namespace WebMaze.Models.Life
{
    public class HouseDestroyedInFireViewModel
    {
        public long Id { get; set; }
        public long AccidentId { get; set; }
        public long HouseAddressId { get; set; }
        public string HouseAddressText { get; set; }
        public IEnumerable<SelectListItem> HouseAddressesList { get; set; }
    }
}
