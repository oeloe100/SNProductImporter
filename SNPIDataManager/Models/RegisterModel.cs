using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models
{
    public class RegisterModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Adress")]
        [Required(ErrorMessage = "An Email Address is Required!")]
        [StringLength(180)]
        public string Email { get; set; }

        [PasswordPropertyText]
        [Required(ErrorMessage = "Password is Required!")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be atleast 8 characters long, With a Uppercase and Lowercase character. And atleast one special character.")]
        [Compare("Password", ErrorMessage = "Password must be same as Confirm Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}