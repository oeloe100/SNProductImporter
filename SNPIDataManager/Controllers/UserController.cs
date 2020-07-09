using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace SNPIDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            string isVerifiedUser = User.Identity.IsAuthenticated.ToString();

            return new string[] { userId, isVerifiedUser };
        }
    }
}
