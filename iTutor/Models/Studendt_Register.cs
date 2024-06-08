using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iTutor.Models
{
    public class Studendt_Register
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please Enter First Name ")]
        [DisplayName("First Name")]
        [StringLength(15, MinimumLength = 2)]
        public string firstname { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please Enter Last Name ")]
        [StringLength(15, MinimumLength = 3)]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Please Enter Address ")]
        [StringLength(50, MinimumLength = 10)]
        [DisplayName("Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Please Enter State ")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("State")]
        public string state { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Please Enter City ")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Gender")]
        public string gender { get; set; }

        [DisplayName("Profile")]
        [Required(ErrorMessage = "Profile pic is required.")]
        public string profile { get; set; }

        [DisplayName("Email")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        //ErrorMessage = "Please enter correct email address")]
        [EmailAddress(ErrorMessage = "Please Enter Email Address")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(16, MinimumLength = 8)]
        [DisplayName("Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
        [DisplayName("Confirm Password")]
        public string cpassword { get; set; }

    }
}