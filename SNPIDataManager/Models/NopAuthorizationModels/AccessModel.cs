using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopAuthorizationModels
{
    public class AccessModel
    {
        public TokenAuthorizationModel TokenAuthorizationModel { get; set; }
        public UserAccessModel UserAccessModel { get; set; }
    }
}