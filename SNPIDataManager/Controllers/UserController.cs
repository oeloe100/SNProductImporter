using Microsoft.AspNet.Identity;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SNPIDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public IEnumerable<string> Get()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            string isVerifiedUser = User.Identity.IsAuthenticated.ToString();

            return new string[] { userId, isVerifiedUser };
        }
    }
}
