using System.Linq;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class HouseDestroyedInFireRepository : BaseRepository<HouseDestroyedInFire>
    {
        public HouseDestroyedInFireRepository(WebMazeContext context) : base(context)
        {
        }

        public HouseDestroyedInFire GetByHouseId(long houseId)
        {
            return dbSet.SingleOrDefault(x => x.DestroyedHouseAddress.Id == houseId);
        }
    }
}
