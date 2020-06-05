using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Managers;
using SNPIDataManager.Models.NopAuthorizationModels;
using SNPIDataManager.Models.NopAuthorizationParametersModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Threading;
using System.Web.Routing;
using SNPIDataManager.Models;
using SNPIDataManager.Helpers;
using SNPIDataManager.Setup;
using System.Threading.Tasks;

namespace SNPIDataManager.Controllers
{
    [Authorize]
    public class NopAuthorizationController : Controller
    {
        UserInformation userInformation = new UserInformation();
        NopAccessSetup setup = new NopAccessSetup();

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

                    int recordsCreated;

                    if (setup.IsSetup())
                    {
                        // *** SAVE CREDENTIALS TO DATABASE ***//
                        recordsCreated = CredentialsProcessor.InsertCredentials
                        (
                            userInformation.UserId(),
                            model.ClientId,
                            model.ClientSecret,
                            model.ServerUrl,
                            model.RedirectUrl
                        );
                    }

                    //*** DONT SAVE ANYWHERE ***//
                    var state = Guid.NewGuid();
                    Session["state"] = state;

                    //*** Nop Authorization Url + Redirect Url ***/
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
        public async Task<ActionResult> ReceiveAccessToken(string code, string state)
        {
            string clientId = "", clientSecret = "", serverUrl = "", redirectUrl = "";

            var authorizationResponseModel = new AuthorizationResponseModel();

            if (ModelState.IsValid &&
                state == Session["state"].ToString())
            {
                if (!string.IsNullOrEmpty(code) &&
                    !string.IsNullOrEmpty(state))
                {
                    var accessModel = new AccessModel();

                    authorizationResponseModel._code = code;
                    authorizationResponseModel._state = state;

                    try
                    {
                        var data = CredentialsProcessor.LoadCredentials<ClientModel>();

                        //*** Loop Trough data (LoadCred<UserModel>) and assign local variables with value from Database! ***//
                        foreach (var row in data)
                        {
                            clientId = row.ClientId;
                            clientSecret = row.ClientSecret;
                            serverUrl = row.ServerUrl;
                            redirectUrl = row.RedirectUrl;
                        }

                        //*** Populate Authentication Parameters to build Response URL ***//
                        var PopulatedAuthenticationModel = PopulateModels.PopulateModels.PopulateAuthenticationModel(clientId, clientSecret, serverUrl, redirectUrl, "authorization_code", code);
                        
                        var nopAuthorizationManager = new NopAuthorizationManager(PopulatedAuthenticationModel.ClientId, PopulatedAuthenticationModel.ClientSecret, PopulatedAuthenticationModel.ServerUrl);                      
                        
                        string responseJSON = await nopAuthorizationManager.GetAuthorizationData(PopulatedAuthenticationModel);
                        
                        TokenAuthorizationModel tokenAuthorizationModel = JsonConvert.DeserializeObject<TokenAuthorizationModel>(responseJSON);

                        accessModel.tokenAuthorizationModel = tokenAuthorizationModel;

                        //*** Populate UserAccessModel with new information ***//
                        PopulateModels.PopulateModels.PopulateUserAccessModel(accessModel, clientId, clientSecret, serverUrl, redirectUrl);

                        string userId = userInformation.UserId();

                        int recordsCreated = CredentialsProcessor.InsertToken
                        (
                            userId,
                            tokenAuthorizationModel.AccessToken,
                            tokenAuthorizationModel.RefreshToken
                        );
                    }
                    catch (Exception ex)
                    {
                       return BadRequest(ex.ToString());
                    }

                    return View(accessModel);
                }
            }

            return BadRequest();
        }

        private ActionResult BadRequest(string message = "Bad Request")
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
        }
    }
}