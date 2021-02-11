using System.Linq;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class CriminalOffenderRepository : BaseRepository<CriminalOffender>
    {
        public CriminalOffenderRepository(WebMazeContext context) : base(context)
        {
        }

        public bool hasCitizenAndAccident(long citizenId, long accidentId)
        {
            return dbSet.Any(x => x.Offender.Id == citizenId
                && x.Accident.Id == accidentId);
        }
    }
}
