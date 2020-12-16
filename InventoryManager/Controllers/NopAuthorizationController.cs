using DataLibrary.BusinessLogic;
using InventoryManager.Builder;
using InventoryManager.ModelManager;
using InventoryManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace InventoryManager.Controllers
{
    public class NopAuthorizationController : Controller
    {
        private readonly IOptions<NopAccessDataPoco> _iOptions;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HttpContextAccessor _accessor;

        public NopAuthorizationController(
            IOptions<NopAccessDataPoco> iOptions,
            IConfiguration configuration, 
            UserManager<IdentityUser> userManger)
        {
            _iOptions = iOptions;
            _configuration = configuration;
            _userManager = userManger;
            _accessor = new HttpContextAccessor();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConnectionCredentialsModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                try
                {
                    var nopAuthorizationManager = new NopAuthorizationBuilder(model.Key, model.Secret, model.ServerUrl);
                    
                    string scheme = _accessor.HttpContext.Request.Scheme + "://" + _accessor.HttpContext.Request.Host + "/";
                    var redirectScheme = scheme + "NopAuthorization/Callback";

                    //*** Save Authorization data to DB **//
                    AuthorizationCredentialsProcessor.InsertAuthorizationCredentials(
                        SetAuthorizationCredentialsData.SetData(user.Id, model.Name, model.Key, model.Secret, model.ServerUrl, redirectScheme), 
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

        public async Task<IActionResult> Callback(string code, string state)
        {
            if (ModelState.IsValid && 
                !string.IsNullOrEmpty(code) &&
                !string.IsNullOrEmpty(state) &&
                state == HttpContext.Session.GetString("state"))
            { 
                try
                {
                    var nopAccessData = AuthorizationCredentialsProcessor.LoadLastAccessData(
                        _configuration.GetConnectionString("DefaultConnection"));
                    var lastUsedData = nopAccessData[^1];

                    var test = lastUsedData.NopSecret.ToString();

                    var nopAuthorizationManager = new NopAuthorizationBuilder(lastUsedData.NopKey, lastUsedData.NopSecret, lastUsedData.ServerUrl);

                    AuthorizationParametersModel model = new AuthorizationParametersModel
                    {
                        ClientId = lastUsedData.NopKey,
                        ClientSecret = lastUsedData.NopSecret,
                        GrantType = "authorization_code",
                        CallbackUrl = _iOptions.Value.CallbackUrl,
                        ServerUrl = lastUsedData.ServerUrl,
                        Code = code
                    };

                    var response = await nopAuthorizationManager.GetAuthorizationData(model);

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
