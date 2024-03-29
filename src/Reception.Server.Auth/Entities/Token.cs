﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reception.Server.Auth.Entities
{
    [Table("Token", Schema = "Auth")]
    public class Token
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}