using System.Linq;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class HouseDestroyedInFireRepository : BaseRepository<HouseDestroyedInFire>
    {
        public HouseDestroyedInFireRepository(WebMazeContext context) : base(context)
        {
        }

        public bool hasHouseAndAccident(long houseAddressId, long accidentId)
        {
            return dbSet.Any(x => x.DestroyedHouseAddress.Id == houseAddressId 
                && x.Accident.Id == accidentId);
        }
    }
}
