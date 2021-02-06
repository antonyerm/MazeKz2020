using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;
using WebMaze.DbStuff.Model;
using WebMaze.Models.Account;

namespace WebMaze.Models.Life
{
    public class AccidentDetailsViewModel
    {
        public long Id { get; set; }
        public DateTime AccidentDate { get; set; }
        public int SelectedAccidentCategory { get; set; } // for switching the 2 parts of view
        public string AccidentCategoryText { get; set; }

        public AdressViewModel AccidentAddressViewModel { get; set; }
        public string AccidentAddressText { get; set; }
        public string AccidentDescription { get; set; }

        public virtual ICollection<AccidentVictimViewModel> AccidentVictimsViewModel { get; set; }

        // Fire
        public FireDetailViewModel FireDetailViewModel { get; set; }

        public ICollection<HouseDestroyedInFireViewModel> HousesDestroyedInFireViewModel { get; set; }

        // Criminal Offence
        public ICollection<CriminalOffenceArticleViewModel> CriminalOffenceArticlesViewModel { get; set; }
        public IEnumerable<SelectListItem> CriminalOffenceArticlesList { get; set; }
        public ICollection<CriminalOffenderViewModel> CriminalOffendersViewModel { get; set; }

        public string ReturnUrl { get; set; }
    }
}
