using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Areas.EDCFeed.Models.ProductModels.TestModels;

namespace SNPIDataManager.Areas.EDCFeed.Controllers.API
{
    [Authorize]
    public class InventoryDataController : ApiController
    {
        /// <summary>
        /// Local FeedPath Bv. C:/ Etc. (Use this only in Development)
        /// </summary>
        private string feedPath = "C:/Users/sexxnation/Downloads/TEMP/eg_xml_feed_2015_nl.xml";

        //For production download xml data from url below. Like
        //Download Limit by EDC is 4x A Day. Scedual download for once a day. Like 24:00.
        //using (System.Net.WebClient client = new System.Net.WebClient())
        //{
        //    client.DownloadFile("http://api.edc.nl/b2b_feed.php?key=4500c66ct0e0w63c8r4129tc80e622rr&sort=xml&type=xml&lang=nl&version=2015", "some.xml");
        //}

        /// <summary>
        /// Build Supplier Categories and return to View.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, List<CategoryModel>> CategoryBuilder()
        {
            XmlDocument edcFeed = new XmlDocument();
            edcFeed.Load(feedPath);

            XmlElement root = edcFeed.DocumentElement;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CategoryModelList> ParentedCategories = EDCCategoriesHelper.CategorizeNodes(root).ToList();
            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;

            IDictionary<string, List<CategoryModel>> RelationToView = new Dictionary<string, List<CategoryModel>>();
            EDCCategoriesHelper.ChildParentRelationForView(ParentedCategories, RelationToView);

            return RelationToView;
        }
    }
}
