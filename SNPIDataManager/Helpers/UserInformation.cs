using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SNPIDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Helpers
{
    public class UserInformation
    {
        private readonly ApplicationUser _User;

        public UserInformation()
        {
            _User = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName
                    (HttpContext.Current.User.Identity.GetUserName());

            UserId();
        }

        public string UserId()
        {
            if (_User != null)
                return _User.Id;
            else
                return null;
        }
    }
}