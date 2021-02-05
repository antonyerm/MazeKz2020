using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models.Life
{
    public class CriminalOffenceArticleViewModel
    {
        public int? SelectedCriminalOffenceArticle { get; set; }
        public string CriminalOffenceArticleText { get; set; }
    }
}
