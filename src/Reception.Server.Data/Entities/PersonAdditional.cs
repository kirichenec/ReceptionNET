using Reception.Model.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.Data.Entities
{
    [Table("PersonAdditional", Schema = "Person")]
    public class PersonAdditional : IUnique
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PhotoId { get; set; }
    }
}