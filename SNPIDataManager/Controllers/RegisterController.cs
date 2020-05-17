using System;
using System.Collections.Generic;
using SNPIDataManager.Helpers;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNPIDataManager.Models;
using System.Threading.Tasks;
using SNPIHelperLibrary;

namespace SNPIDataManager.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> _Register(PreLoginModel model)
        {
            if (ModelState.IsValid) {
                var helperInstance = new APIHelper();
                try
                {
                    var result = await helperInstance.Registrate
                        (
                            model._RegisterModel.Email,
                            model._RegisterModel.Password,
                            model._RegisterModel.ConfirmPassword
                        );

                    return await Task.Run(() => View("~/Views/Home/Index.cshtml"));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LoginErrMessage", ex.Message);
                    ModelState.AddModelError("LoginErrDescription", DMHelper.ErrBuilder(ex.Message));

                    return await Task.Run(() => View("./index"));
                }
            }

            return await Task.Run(() => View("./index"));
        }
    }
}