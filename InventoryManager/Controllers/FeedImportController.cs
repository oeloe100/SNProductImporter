using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManager.Controllers
{
    public class FeedImportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
