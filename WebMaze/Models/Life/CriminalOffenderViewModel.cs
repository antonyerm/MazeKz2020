using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMaze.Models.Life
{
    public class CriminalOffenderViewModel
    {
        public long Id { get; set; }
        public long AccidentId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, выберите правонарушителя в списке")]
        [Remote(action: "IsOffenderUnique", controller: "Life", 
            AdditionalFields = nameof(AccidentId) + "," + nameof(InitialCitizenId), 
            ErrorMessage = "Такой правонарушитель уже указывался")]
        public long? CitizenId { get; set; }
        public long? InitialCitizenId { get; set; }
        public string OffenderName { get; set; }
        public IEnumerable<SelectListItem> CitizenList { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите приговор")]
        public string Verdict { get; set; }
    }
}
