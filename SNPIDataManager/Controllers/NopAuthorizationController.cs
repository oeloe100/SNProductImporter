using SNPIDataManager.Models.NopAuthorizationModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    throw new NotImplementedException();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        public ActionResult BadRequest(string message = "Bad Request")
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
        }
    }
}