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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ConnectionCredentialsModel model)
        {
            Console.WriteLine();

            if (ModelState.IsValid)
            {
                try
                {
                    var nopAuthorizationManager = new NopAuthorizationBuilder(model.Key, model.Secret, model.ServerUrl);
                    string callbackUrl = Request.Scheme + "/Callback";

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
    }
}
