using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Model.Dto
{
    public class VisitorInfoDto : PersonDto
    {
        public DateTime IncomingDate { get; set; }

        public string Message { get; set; }
    }
}
