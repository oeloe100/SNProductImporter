using Newtonsoft.Json;
using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public class NopShopCategorizationHelper
    {
        public async Task<IDictionary<ProductModel, List<ProductModel>>> NopCategoriesResource(string accessToken, string serverUrl)
        {
            var clientHelper = new NopAPIClientHelper(accessToken, serverUrl);

            string jsonUrl = $"/api/categories";
            object customerData = await clientHelper.Get(jsonUrl);

            var categoriesRootObject = JsonConvert.DeserializeObject<CategoriesRootObject>(customerData.ToString());
            var categories = categoriesRootObject.Categories.Where(categorie => !string.IsNullOrEmpty(categorie.Name));

            /*** Model To View Dictionary ***/
            IDictionary<ProductModel, List<ProductModel>> MTVDictionary = new Dictionary<ProductModel, List<ProductModel>>();

            /*** Order Categories (Parent > Child etc..) ***/
            PopulateDictionaries(categories, MTVDictionary);

            return MTVDictionary;
        }

        private void PopulateDictionaries(IEnumerable<CategoriesModel> categories,
            IDictionary<ProductModel,
            List<ProductModel>> MTVDictionary)
        {
            string id = "";
            string parentId = "";

            ProductModel previousModel = new ProductModel();

            foreach (var index in categories)
            {
                if (index.ParentId == "0")
                {
                    previousModel = new ProductModel()
                    {
                        Id = index.Id,
                        Title = index.Name
                    };

                    var setup = MTVDictionary[previousModel] = new List<ProductModel>();

                    id = index.Id;
                    parentId = index.ParentId;
                }
                else if (index.ParentId != "0")
                {
                    if (index.ParentId == id)
                    {
                        var ChildProductModel = SetProductModel(index.Id, index.Name);

                        if (!MTVDictionary.ContainsKey(previousModel))
                        {
                            MTVDictionary[previousModel].Add(ChildProductModel);
                        }
                        else
                        {
                            MTVDictionary[previousModel].Add(ChildProductModel);
                        }

                        id = index.Id;
                    }
                }
            }
        }

        private ProductModel SetProductModel(string id, string name)
        {
            var keyProductModel = new ProductModel()
            {
                Id = id,
                Title = name,
            };

            return keyProductModel;
        }
    }
}