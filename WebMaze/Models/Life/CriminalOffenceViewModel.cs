using System.Collections.Generic;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.Models.Life
{
    public class CriminalOffenceViewModel
    {
        public long AccidentId { get; set; }
        public List<CriminalCodeEnum> OffenceArticleCode { get; set; }
        public List<CriminalOffenderViewModel> Offender { get; set; }
    }
}
