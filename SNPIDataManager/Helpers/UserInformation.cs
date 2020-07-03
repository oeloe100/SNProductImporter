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
        ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName
                    (HttpContext.Current.User.Identity.GetUserName());

        public UserInformation()
        {
            UserId();
        }

        public string UserId()
        {
            if (!string.IsNullOrEmpty(user.Id))
                return user.Id;
            else
                return null;
        }
    }
}