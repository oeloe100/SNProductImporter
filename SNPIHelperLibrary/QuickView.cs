using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPIHelperLibrary
{
    public static class DMHelper
    {
        public static string SelectQuickPartialView(string type)
        {
            switch (type)
            {
                case "Login":
                    return "~/Views/Shared/_LoginForm.cshtml";
                case "Logout":
                    return "~/Views/Shared/_LogoutForm.cshtml";

            }

            return "Invalid Type: " + type;
        }

        public static string ErrBuilder(string component)
        {
            switch (component)
            {
                case "Bad Request":
                    return "Wrong Username or Password";
                case "":
                    return "Oops Something Went Wrong?";
            }

            return "Invalid type: " + component;
        }
    }
}
