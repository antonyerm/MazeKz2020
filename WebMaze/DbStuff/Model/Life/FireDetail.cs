namespace WebMaze.DbStuff.Model.Life
{
    public class FireDetail : BaseModel
    {
        public virtual FireCauseEnum? FireCause { get; set; }
        public virtual FireClassEnum? FireClass { get; set; }
        public virtual long AccidentId { get; set; }

        // relations
        // The dependent side of 1:1 
        public virtual Accident Accident { get; set; }
    }
}
