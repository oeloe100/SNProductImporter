using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManager.Builder;
using InventoryManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Controllers
{
    public class NopAuthorizationController : Controller
    {
        private HttpContextAccessor _accessor;

        public NopAuthorizationController()
        {
            _accessor = new HttpContextAccessor();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ConnectionCredentialsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nopAuthorizationManager = new NopAuthorizationBuilder(model.Key, model.Secret, model.ServerUrl);
                    string scheme = _accessor.HttpContext.Request.Scheme + "://" + _accessor.HttpContext.Request.Host + "/";

                    /*
                    int recordsCreated;

                    if (_Setup.IsSetup())
                    {
                        recordsCreated = CredentialsProcessor.InsertUserCredentials
                        (
                            _UserInformation.UserId(),
                            model.ClientId,
                            model.ClientSecret,
                            model.ServerUrl,
                            model.RedirectUrl
                        );
                    }
                    */

                    //*** DONT SAVE ANYWHERE ***//
                    var state = Guid.NewGuid();
                    HttpContext.Session.SetString("state", state.ToString());

                    var redirectScheme = scheme + "NopAuthorization/Callback";

                    //*** Nop Authorization Url + Redirect Url ***/
                    var authorizationUrl = nopAuthorizationManager.GetAuthorizationUrl(redirectScheme, new string[] { }, state.ToString());

                    return Redirect(authorizationUrl);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        public IActionResult Callback(string code, string state)
        {
            Console.WriteLine();

            if (state == HttpContext.Session.GetString("state"))
            { 
                try
                {
                    Console.WriteLine();
                    return Ok(200);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            return BadRequest();
        }
    }
}
