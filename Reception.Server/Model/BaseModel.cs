using Reception.Model.Interfaces;

namespace Reception.Server.Model
{
    public class BaseModel : IUnique
    {
        public string Comment { get; set; }

        public int Id { get; set; }
    }
}