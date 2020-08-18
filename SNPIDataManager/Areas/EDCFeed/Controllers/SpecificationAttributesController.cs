using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Areas.EDCFeed.Models.ProductSpecificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNPIDataManager.Areas.EDCFeed.Controllers
{
    public class SpecificationAttributesController : Controller
    {
        private ProductSpecificationAttributeDataModel _ProductSpecAttrDataModel;

        // GET: EDCFeed/SpecificationAttributes
        public ActionResult Index()
        {
            _ProductSpecAttrDataModel = new ProductSpecificationAttributeDataModel()
            {
                ProductSpecAttributesWithValues = InventoryDataHelper.ProductSpecificationAttributesSelector()
            };

            return View(_ProductSpecAttrDataModel);
        }
    }
}