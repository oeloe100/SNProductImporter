using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNPIDataManager.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : ApiController
    {
        //GET api/Registration/ReturnUsers
        [HttpGet]
        [ActionName("ReturnUsers")]
        public IEnumerable<string> Get()
        {
            return new string[] { "User1", "User2" };
        }
    }
}
