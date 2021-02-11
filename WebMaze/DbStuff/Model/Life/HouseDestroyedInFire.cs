namespace WebMaze.DbStuff.Model.Life
{
    public class HouseDestroyedInFire : BaseModel
    {
        public virtual long AccidentId { get; set; }

        // relations
        // the dependent side of 1:1
        public virtual Accident Accident { get; set; }
        // the N side of 1:N
        public virtual Adress DestroyedHouseAddress { get; set; }
    }
}
