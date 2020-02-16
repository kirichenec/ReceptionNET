using Reception.Model.Interfaces;

namespace Reception.Server.Data.Model
{
    public class BaseModel : IUnique
    {
        public string Comment { get; set; }

        public int Id { get; set; }
    }
}