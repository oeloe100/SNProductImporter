﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPIDataLibrary.Models
{
    public class ClientModel
    {
        public string UserId { get; set; }
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ServerUrl { get; set; }

        public string RedirectUrl { get; set; }
    }
}
