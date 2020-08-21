using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SNPIDataManager.Helpers;
using SNPIDataManager.Models;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml;

namespace SNPIDataManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _LoginPartialView;

        public HomeController()
        {
            _LoginPartialView = "~/Views/Shared/_LoginForm.cshtml";
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Welcome Home!";

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(PreLoginModel user)
        {
            //log4net.ILog logger = log4net.LogManager.GetLogger("FileAppender");
            //logger.Error("Testlog: Error logging!");

            if (ModelState.IsValid)
            {

                var helperInstance = new APIHelper();
                try
                {
                    var result = await helperInstance.Authenticate(user.LoginModel.Username, user.LoginModel.Password);

                    if (result.AuthenticatedUser.Username != null &&
                        result.AuthenticatedUser.Access_Token != null)
                    {
                        FormsAuthentication.SetAuthCookie(result.AuthenticatedUser.Username, false);

                        return await Task.Run(() => View(result));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LoginErrMessage", ex.Message);
                    ModelState.AddModelError("LoginErrDescription", DMHelper.ErrBuilder(ex.Message));

                    return await Task.Run(() => this.View(_LoginPartialView));
                }
            }

            ModelState.AddModelError("LoginErrDescription", DMHelper.ErrBuilder(""));
            return await Task.Run(() => this.View(_LoginPartialView));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}