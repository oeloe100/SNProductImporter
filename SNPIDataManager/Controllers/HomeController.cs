using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using SNPIDataManager.Helpers;
using SNPIDataManager.Models;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
        string loginPartialView = DMHelper.SelectQuickPartialView("Login");

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

                    if (result._AuthenticatedUser.Username != null &&
                        result._AuthenticatedUser.Access_Token != null)
                    {
                        FormsAuthentication.SetAuthCookie(result._AuthenticatedUser.Username, false);
                        return await Task.Run(() => View(result));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LoginErrMessage", ex.Message);
                    ModelState.AddModelError("LoginErrDescription", DMHelper.ErrBuilder(ex.Message));

                    return await Task.Run(() => this.View(loginPartialView));
                }
            }

            ModelState.AddModelError("LoginErrDescription", DMHelper.ErrBuilder(""));
            return await Task.Run(() => this.View(loginPartialView));
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}