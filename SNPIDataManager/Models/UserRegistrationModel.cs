using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SNPIDataManager.Models
{
    [AllowAnonymous]
    public class UserRegistrationModel
    {
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string phoneNumber { get; set; }
        
    }
}