using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class FireDetailRepository : BaseRepository<FireDetail>
    {
        public FireDetailRepository(WebMazeContext context) : base(context)
        {
        }
    }
}
