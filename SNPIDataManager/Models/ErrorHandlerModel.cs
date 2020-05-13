using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models
{
    public class ErrorHandlerModel
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}