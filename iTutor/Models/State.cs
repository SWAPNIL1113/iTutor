using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iTutor.Models
{
    public class State
    {
        [Key]
        [MaxLength(3)]
        public string code { get; set; }

        [Required]
        [MaxLength(50)]
        public string state { get; set; }

    }
}