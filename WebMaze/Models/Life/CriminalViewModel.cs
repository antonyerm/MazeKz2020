using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.Models.Life
{
    public class CriminalViewModel
    {
        public long AccidentId { get; set; }
        public CriminalCodeEnum OffenceArticleCode { get; set; }
        public string OffenceArticle { get; set; }
        public List<OffenderViewModel> Offender { get; set; }
        public List<AccidentVictimViewModel> Victim { get; set; }
    }
}
