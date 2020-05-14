using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models
{
    public class AuthenticatedUser
    {
        public string Access_Token { get; set; }
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}