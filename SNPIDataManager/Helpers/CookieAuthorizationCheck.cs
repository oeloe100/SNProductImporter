using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace SNPIDataManager.Helpers
{
    public static class CookieAuthorizationCheck
    {
        public static bool CookieAuthCheck()
        {
            string cookiename = FormsAuthentication.FormsCookieName;

            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                Console.WriteLine();
                return true;
            }

            Console.WriteLine();
            return false;
        }
    }
}