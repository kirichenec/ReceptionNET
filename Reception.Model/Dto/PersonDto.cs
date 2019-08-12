using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Model.Dto
{
    public class PersonDto
    {
        public string Comment { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string PhotoPath { get; set; }

        public string Post { get; set; }

        public string SecondName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Uid { get; set; }
    }
}
