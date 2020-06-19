using Newtonsoft.Json;
using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public class NopShopCategorizationHelper
    {
        public async Task<IEnumerable<CategoriesModel>> NopCategoriesResource(string accessToken, string serverUrl)
        {
            var clientHelper = new NopAPIClientHelper(accessToken, serverUrl);

            string jsonUrl = $"/api/categories";
            object customerData = await clientHelper.Get(jsonUrl);

            var categoriesRootObject = JsonConvert.DeserializeObject<CategoriesRootObject>(customerData.ToString());
            var categoriesIE = categoriesRootObject.Categories.Where(categorie => !string.IsNullOrEmpty(categorie.Name));

            List<CategoriesModel> categoriesList = categoriesIE.ToList();

            Sort(categoriesList);

            return categoriesIE;
        }

        private void Sort(List<CategoriesModel> categoriesUnsorted) 
        {
            IDictionary<string, List<CategoriesModel>> categoriesSorted = new Dictionary<string, List<CategoriesModel>>();

            for (var i = 0; i < categoriesUnsorted.Count(); i++)
            {
                int id = Int16.Parse(categoriesUnsorted[i].Id);
                int parentId = Int16.Parse(categoriesUnsorted[i].ParentId);

                SortByName(categoriesUnsorted[i], categoriesSorted, id, parentId);
            }
        }

        private void SortByName(CategoriesModel unsorted, IDictionary<string, List<CategoriesModel>> sorted, int id, int parentId)
        {
            List<CategoriesModel> sortedModel = new List<CategoriesModel>();

            if (parentId <= 0)
            {
                sortedModel.Add(SortedModel(unsorted));
                sorted.Add(unsorted.Id, sortedModel);
            }
            else if (parentId > 0)
            {
                var key = unsorted.ParentId;
                SortNested(unsorted, sorted, key);
            }
        }

        private void SortNested(CategoriesModel unsorted, IDictionary<string, List<CategoriesModel>> sorted, string key)
        {
            if (sorted.ContainsKey(key))
            {
                for (var x = 0; x < sorted[key].Count; x++)
                {
                    var model = sorted[key];
                    var nestedModel = model[x].NestedModel;

                    nestedModel = new Dictionary<string, CategoriesModel>();
                    nestedModel.Add(unsorted.Id, SortedModel(unsorted));

                    //Nestedmodel is being created but not added to sorted.nestedlist. Fix this.
                    Console.WriteLine();
                }
            }
        }

        private CategoriesModel SortedModel(CategoriesModel unsorted)
        {
            var sortedItemModel = new CategoriesModel()
            {
                Id = unsorted.Id,
                ParentId = unsorted.ParentId,
                Name = unsorted.Name
            };

            return sortedItemModel;
        }
    }
}