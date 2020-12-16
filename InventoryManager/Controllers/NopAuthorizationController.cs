using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.BusinessLogic;
using InventoryManager.Builder;
using InventoryManager.ModelManager;
using InventoryManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InventoryManager.Controllers
{
    public class NopAuthorizationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private HttpContextAccessor _accessor;

        public NopAuthorizationController(
            IConfiguration configuration, 
            UserManager<IdentityUser> userManger)
        {
            _configuration = configuration;
            _userManager = userManger;
            _accessor = new HttpContextAccessor();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ConnectionCredentialsModel model)
        {
            var user = _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                try
                {
                    var nopAuthorizationManager = new NopAuthorizationBuilder(model.Key, model.Secret, model.ServerUrl);
                    
                    string scheme = _accessor.HttpContext.Request.Scheme + "://" + _accessor.HttpContext.Request.Host + "/";
                    var redirectScheme = scheme + "NopAuthorization/Callback";

                    AuthorizationCredentialsProcessor.InsertAuthorizationCredentials(
                        SetAuthorizationCredentialsData.SetData(user.Id.ToString(), model.Name, model.Key, model.Secret, model.ServerUrl, redirectScheme), 
                        _configuration.GetConnectionString("DefaultConnection"));

                    //*** DONT SAVE ANYWHERE ***//
                    var state = Guid.NewGuid();
                    HttpContext.Session.SetString("state", state.ToString());

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
