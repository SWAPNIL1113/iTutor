using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iTutor.Models
{
    public class Class
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please Enter Standard ")]
        [DisplayName("Enter Standard")]
        public string std { get; set; }
    }
}