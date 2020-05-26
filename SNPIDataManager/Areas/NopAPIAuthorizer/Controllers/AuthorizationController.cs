using SNPIDataManager.Areas.NopAPIAuthorizer.Managers;
using SNPIDataManager.Areas.NopAPIAuthorizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;

namespace SNPIDataManager.Areas.NopAPIAuthorizer.Controllers
{
    [Authorize]
    public class AuthorizationController : Controller
    {
        // GET: NopAPIAuthorizer/Authorization
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(UserAccessModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nopAuthorizationManager = new AuthorizationManager(model.ClientId, model.ClientSecret, model.ServerUrl);
                    var redirectUrl = Url.RouteUrl("GetAccessToken", null, Request.Url.Scheme);

                    Console.WriteLine();

                    if (redirectUrl != model.RedirectUrl)
                    {
                        return BadRequest();
                    }

                    // *** CURRENT SESSION INSTANCE ***//
                    Session["clientId"] = model.ClientId;
                    Session["clientSecret"] = model.ClientSecret;
                    Session["serverUrl"] = model.ServerUrl;
                    Session["redirectUrl"] = redirectUrl;

                    // *** SAVE CREDENTIALS TO DATABASE ***//
                    int recordsCreated = CredProcessor.InsertCredentials
                    (
                        model.ClientId,
                        model.ClientSecret,
                        model.ServerUrl,
                        model.RedirectUrl
                    );

                    //*** DONT SAVE ANYWHERE ***//
                    var state = Guid.NewGuid();
                    Session["state"] = state;

                    Console.WriteLine();

                    string authUrl = nopAuthorizationManager.BuildAuthUrl(redirectUrl, new string[] { }, state.ToString());

                    return Redirect(authUrl);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        [HttpGet]
        public ActionResult GetAccessToken(string code, string state)
        {
            string clientId = "", clientSecret = "", serverUrl = "", redirectUrl = "";

            if (ModelState.IsValid && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(state))
            {
                if (state != Session["state"].ToString())
                {
                    return BadRequest();
                }

                var model = new AccessModel();
                var data = CredProcessor.LoadCredentials<UserModel>();
                string grantType = "authorization_code";

                try
                {
                    //*** Loop Trough data (LoadCred<UserModel>) and assign local variables with value from Database! ***//
                    foreach (var row in data)
                    {
                        clientId = row.ClientId;
                        clientSecret = row.ClientSecret;
                        serverUrl = row.ServerUrl;
                        redirectUrl = row.RedirectUrl;
                    }

                    //*** Populate Authentication Parameters to build Response URL ***//
                    var PopulatedAuthenticationModel = PopulateModels.PopulateModels.PopulateAuthenticationModel(null, clientId, clientSecret, serverUrl, redirectUrl, grantType, code);

                    var nopAuthorizationManager = new AuthorizationManager(PopulatedAuthenticationModel.ClientId, PopulatedAuthenticationModel.ClientSecret, PopulatedAuthenticationModel.ServerUrl);
                    string responseJson = nopAuthorizationManager.GetAuthorizationData(PopulatedAuthenticationModel);
                    AuthorizationModel authorizationModel = JsonConvert.DeserializeObject<AuthorizationModel>(responseJson);

                    model.AuthorizationModel = authorizationModel;

                    //*** Populate UserAccessModel with new information ***//
                    PopulateModels.PopulateModels.PopulateUserAccessModel(model, clientId, clientSecret, serverUrl, redirectUrl);

                    // TODO: Here you can save your access and refresh tokens in the database. For illustration purposes we will save them in the Session and show them in the view.
                    Session["accessToken"] = authorizationModel.AccessToken;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return View("~/Views/Connect/TokenForm.cshtml", model);
            }

            return BadRequest();
        }

        private ActionResult BadRequest(string message = "Bad Request")
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
        }
    }
}