using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.Models.Life
{
    public class CriminalOffenceArticleViewModel
    {
        public long Id { get; set; }
        public long AccidentId { get; set; }
        [Required(ErrorMessage ="Пожалуйста, выберите статью кодекса")]
        public CriminalCodeEnum? CriminalOffenceArticleEnum { get; set; }
        public string CriminalOffenceArticleText { get; set; }
        public IEnumerable<SelectListItem> CriminalOffenceArticlesList { get; set; }
        
    }
}
