 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iTutor.Models
{
    public class City
    {
        [Key]
        [MaxLength(3)]
        public int code { get; set; }

        [Required]
        [MaxLength(75)]
        public string city { get; set; }

        [ForeignKey("State")]
        public string statecode { get; set; }

        public virtual State State { get; set; }
 
    }
}