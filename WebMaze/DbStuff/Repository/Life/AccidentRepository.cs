using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class AccidentRepository : BaseRepository<Accident>
    {
        public AccidentRepository(WebMazeContext context) : base(context)
        {
        }
    }
}
