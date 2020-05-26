using SNPIDataManager.Managers;
using SNPIDataManager.Models.NopAuthorizationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SNPIDataManager.Controllers
{
    public class NopAuthorizationController : Controller
    {
        // GET: NopAuthorization
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(UserAccessModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nopAuthorizationManager = new NopAuthorizationManager(model.ClientId, model.ClientSecret, model.ServerUrl);
                    var callbackUrl = Url.RouteUrl("TokenEP", null, Request.Url.Scheme);

                    if (callbackUrl != model.RedirectUrl)
                    {
                        return BadRequest();
                    }

                    // *** CURRENT SESSION INSTANCE ***//
                    Session["clientId"] = model.ClientId;
                    Session["clientSecret"] = model.ClientSecret;
                    Session["serverUrl"] = model.ServerUrl;
                    Session["redirectUrl"] = callbackUrl;

                    //*** DONT SAVE ANYWHERE ***//
                    var state = Guid.NewGuid();
                    Session["state"] = state;

                    var authorizationUrl = nopAuthorizationManager.GetAuthorizationUrl(callbackUrl, new string[] { }, state.ToString());

                    return Redirect(authorizationUrl);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ReceiveAccessToken(string code, string state)
        {
            Console.WriteLine();
            throw new NotImplementedException();
        }

        private ActionResult BadRequest(string message = "Bad Request")
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
        }
    }
}