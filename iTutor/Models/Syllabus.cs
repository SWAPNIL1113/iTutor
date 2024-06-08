using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iTutor.Models
{
    public class Syllabus
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please Select Standard ")]
        [DisplayName("Standard")]
        public string std { get; set; }

        [Required(ErrorMessage = "Please Select Subject")]
        [DisplayName("Subject")]
        public string sub { get; set; }

        [Required(ErrorMessage = "Please Enter Topic Name ")]
        [DisplayName("Topic")]
        public string topic { get; set; }

        [Required(ErrorMessage = "Please Enter Description ")]
        [DisplayName("Description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please Upload Part 1 ")]
        [DisplayName("Part 1")]
        public string image { get; set; }

        [Required(ErrorMessage = "Please Upload Part 2")]
        [DisplayName("Part 2")]
        public string part1 { get; set; }

        [Required(ErrorMessage = "Please Upload Part 3")]
        [DisplayName("Part 3")]
        public string part2 { get; set; }

       
       
    }
}