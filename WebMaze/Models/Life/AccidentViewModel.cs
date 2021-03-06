﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebMaze.Models.Account;

namespace WebMaze.Models.Life
{
    public class AccidentViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Пожалуйста, введите дату")]
        public DateTime AccidentDate { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите тип происшествия")]
        public int? SelectedAccidentCategory { get; set; }
        public string AccidentCategoryText { get; set; }
        public IEnumerable<SelectListItem> AccidentCategoryList { get; set; }

        public AdressViewModel AddressViewModel { get; set; }
        public long? SelectedAccidentAddress { get; set; }
        public string AccidentAddressText { get; set; }
        public IEnumerable<SelectListItem> AccidentAddressList { get; set; }
        
        public string AccidentDescription { get; set; }
    }
}
