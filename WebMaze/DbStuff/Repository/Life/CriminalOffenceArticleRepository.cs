using WebMaze.DbStuff.Model.Life;

namespace WebMaze.DbStuff.Repository.Life
{
    public class CriminalOffenceArticleRepository : BaseRepository<CriminalOffenceArticle>
    {
        public CriminalOffenceArticleRepository(WebMazeContext context) : base(context)
        {
        }
    }
}
