using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class VictimRepository : BaseRepository<AccidentVictim>
    {
        public VictimRepository(WebMazeContext context) : base(context)
        {
        }
    }
}
