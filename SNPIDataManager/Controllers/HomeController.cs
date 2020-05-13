using SNPIDataManager.Helpers;
using SNPIDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace SNPIDataManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel user)
        {
            var helperInstance = new APIHelper();
            var result = await helperInstance.Authenticate(user.Username, user.Password);

            Console.WriteLine(result);

            return await Task.Run(() => View(result));
        }
    }
}

//tonraschenko@hotmail.com
//@TestTest