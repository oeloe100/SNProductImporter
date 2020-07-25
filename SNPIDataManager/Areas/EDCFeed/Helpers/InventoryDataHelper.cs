using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Builder;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public class InventoryDataHelper
    {
        private static readonly XmlDocument _EDCFeed;
        private static XmlElement _Root;
        private static readonly string _FeedPath;
        private static SupplierCategoryBuilder _SupplierCategoryBuilder;
        private static MappingProductBuilder _MappingProductBuilder;

        static InventoryDataHelper()
        {
            _EDCFeed = new XmlDocument();
            _FeedPath = "C:/Users/sexxnation/Downloads/TEMP/eg_xml_feed_2015_nl.xml";
            
            _EDCFeed.Load(_FeedPath);
            _Root = _EDCFeed.DocumentElement;
            
            _SupplierCategoryBuilder = new SupplierCategoryBuilder(_Root, _FeedPath);
            _MappingProductBuilder = new MappingProductBuilder(_FeedPath);
        }

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
        public static IDictionary<string, List<SupplierModel>> CategoryBuilder()
        {
            var items = _SupplierCategoryBuilder.RelationToView;

            return items;
        }

        /// <summary>
        /// Build Product Models to sync with the shop.
        /// </summary>
        /// <returns></returns>
        public static List<JObject> MappingProductBuilder()
        {
            var testData = _MappingProductBuilder.SelectProductsForMapping();

            return testData;
        }

        /// <summary>
        /// Update Selected product Properties
        /// </summary>
        /// <returns></returns>
        public static JObject UpdateProductProperties(int productId, List<int> attributeValuesId, int id, int index)
        {
            var testData = _MappingProductBuilder.UpdateProductModelList(productId, attributeValuesId, id, index);

            return testData;
        }
    }
}