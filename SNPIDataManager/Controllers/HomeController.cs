using Microsoft.Ajax.Utilities;
using SNPIDataManager.Helpers;
using SNPIDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Xml;

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
        public async Task<ActionResult> Login(PreLoginModel user)
        {
            if (ModelState.IsValid)
            {
                var helperInstance = new APIHelper();
                try
                {
                    var result = await helperInstance.Authenticate(user._LoginModel.Username, user._LoginModel.Password);
                    return await Task.Run(() => View(result));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LoginErrMessage", ex.Message);
                    ModelState.AddModelError("LoginErrDescription", "Wrong Username or Password");
                    
                    return await Task.Run(() => this.View("~/Views/Shared/_LoginForm.cshtml"));
                }
            }
            
            return await Task.Run(() => View("Oops someting went wrong? (HCL999)"));
        }
    }
}