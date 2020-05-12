using SNPIDataManager.Helpers;
using SNPIDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SNPIDataManager.Controllers
{
    public class HomeController : Controller
    {
        private IAPIHelper _apiHelper;

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(AuthenticatedUser user)
        {
            var result = await _apiHelper.Authenticate(user.Username, user.Access_Token);

            Console.WriteLine(result.Access_Token);

            Console.WriteLine();

            return await Task.Run(() => View(user));
        }

        /*
        public async Task<AuthenticatedUser> Login()
        {
            var result = await _apiHelper.Authenticate(_username, _password);

            var authenticatedUser = new AuthenticatedUser
            {
                Username = result.Username,
                Access_Token = result.Access_Token
            };

            return authenticatedUser;
        }
        */
    }
}
