using System;
using System.Collections.Generic;
using SNPIDataManager.Helpers;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNPIDataManager.Models;
using System.Threading.Tasks;

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
            var helperInstance = new APIHelper();
            var result = await helperInstance.Registrate
                (
                    model._RegisterModel.Email,
                    model._RegisterModel.Password,
                    model._RegisterModel.ConfirmPassword
                );

            return await Task.Run(() => View("~/Views/Home/Index.cshtml"));
        }
    }
}