using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model.Life
{
    public class CriminalOffenceArticle : BaseModel
    {
        public virtual CriminalCodeEnum OffenceArticle { get; set; }
        public virtual long AccidentId { get; set; }

        // relations
        // Dependent side of 1:1
        public virtual Accident Accident { get; set; }
    }
}
