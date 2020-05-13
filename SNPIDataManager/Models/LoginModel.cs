using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [DisplayName("Email Adress")]
        [Required(ErrorMessage = "An Email Address is Required!")]
        [StringLength(180)]
        public string Username { get; set; }

        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(180, ErrorMessage = "Password exceeded the maxium length of 180 Characters")]
        public string Password { get; set; }
    }
}