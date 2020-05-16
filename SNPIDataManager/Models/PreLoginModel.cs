using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models
{
    public class PreLoginModel
    {
        public RegisterModel _RegisterModel { get; set; }
        public LoginModel _LoginModel { get; set; }
        public AuthenticatedUser _AuthenticatedUser { get; set; }
        public ErrorHandlerModel _ErrorHandlerModel { get; set; }
    }
}