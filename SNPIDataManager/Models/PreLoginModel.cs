using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models
{
    public class PreLoginModel
    {
        public RegisterModel RegisterModel { get; set; }
        public LoginModel LoginModel { get; set; }
        public AuthenticatedUser AuthenticatedUser { get; set; }
        public ErrorHandlerModel ErrorHandlerModel { get; set; }
    }
}