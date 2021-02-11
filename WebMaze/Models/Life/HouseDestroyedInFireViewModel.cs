using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMaze.Models.Life
{
    public class HouseDestroyedInFireViewModel
    {
        public long Id { get; set; }
        public long AccidentId { get; set; }
        [Required(ErrorMessage = "Укажите, пожалуйста, адрес сгоревшего дома")]
        [Remote(action: "IsBurntHouseUnique", controller: "Life", 
            AdditionalFields = nameof(AccidentId) + "," + nameof(InitialHouseAddressId), 
            ErrorMessage = "Такой дом уже указывался")]
        public long? HouseAddressId { get; set; }
        public long? InitialHouseAddressId { get; set; }
        public string HouseAddressText { get; set; }
        public IEnumerable<SelectListItem> HouseAddressesList { get; set; }
    }
}
