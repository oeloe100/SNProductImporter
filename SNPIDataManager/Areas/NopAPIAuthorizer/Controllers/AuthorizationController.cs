using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}