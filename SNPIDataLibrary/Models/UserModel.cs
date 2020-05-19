using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPIDataLibrary.Models
{
    class UserModel
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ServerUrl { get; set; }

        public string RedirectUrl { get; set; }
    }
}
