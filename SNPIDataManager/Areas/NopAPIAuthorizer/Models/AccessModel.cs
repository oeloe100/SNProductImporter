using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.NopAPIAuthorizer.Models
{
    public class AccessModel
    {
        public AuthorizationModel AuthorizationModel { get; set; }
        public UserAccessModel UserAccessModel { get; set; }
    }
}