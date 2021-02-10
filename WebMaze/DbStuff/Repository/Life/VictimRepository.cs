using System;
using System.Linq;
using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class VictimRepository : BaseRepository<AccidentVictim>
    {
        public VictimRepository(WebMazeContext context) : base(context)
        {
        }

        public bool hasCitizenAndAccident(long citizenId,long accidentId)
        {
            return dbSet.Any(x => x.Victim.Id == citizenId 
                && x.Accident.Id == accidentId);
        }
    }
}
