using Reception.Model.Interface;

namespace Reception.Server.Data.Entities
{
    public class PersonAdditional : IUnique
    {
        public int Id { get; set; }

        public int? PhotoId { get; set; }
    }
}
