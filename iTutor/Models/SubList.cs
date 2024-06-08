using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iTutor.Models
{
    public class SubList
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Please Enter Subject Name ")]
        [DisplayName("Subject Name")]
        public string sub { get; set; }

        [Required(ErrorMessage = "Please Select Standard ")]

        [DisplayName("Select Standard")]
        public string std { get; set; }
    }
}