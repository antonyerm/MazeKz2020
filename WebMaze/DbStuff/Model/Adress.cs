using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Model
{
    public class Adress : BaseModel
    {
        public string City { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public virtual CitizenUser Owner { get; set; }

        #region Life Project
        // relations
        public virtual ICollection<HouseDestroyedInFire> HousesDestroyedInFire { get; set; }
        #endregion
    }
}
