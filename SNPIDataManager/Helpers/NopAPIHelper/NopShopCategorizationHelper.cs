using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public static class NopShopCategorizationHelper
    {
        private static CategoriesModel nextEntryModel;

        public static async Task<IDictionary<string, List<CategoriesModel>>> NopCategoriesResource(string accessToken, string serverUrl)
        {
            var clientHelper = new NopAPIClientHelper(accessToken, serverUrl);

            string jsonUrl = $"/api/categories";
            object customerData = await clientHelper.Get(jsonUrl);

            var categoriesRootObject = JsonConvert.DeserializeObject<CategoriesRootObject>(customerData.ToString());
            var categoriesIE = categoriesRootObject.Categories.Where(categorie => !string.IsNullOrEmpty(categorie.Name));

            List<CategoriesModel> categoriesList = categoriesIE.ToList();

            var categoriesSorted = Sort(categoriesList);

            return categoriesSorted;
        }

        private static IDictionary<string, List<CategoriesModel>> Sort(List<CategoriesModel> categoriesUnsorted) 
        {
            IDictionary<string, List<CategoriesModel>> categoriesSorted = new Dictionary<string, List<CategoriesModel>>();

            for (var i = 0; i < categoriesUnsorted.Count(); i++)
            {
                int id = Int16.Parse(categoriesUnsorted[i].Id);
                int parentId = Int16.Parse(categoriesUnsorted[i].ParentId);

                SortByName(categoriesUnsorted[i], categoriesSorted, id, parentId);
            }

            return categoriesSorted;
        }

        private static void SortByName(CategoriesModel unsorted, IDictionary<string, List<CategoriesModel>> sorted, int id, int parentId)
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

        private static void SortNested(CategoriesModel unsorted, IDictionary<string, List<CategoriesModel>> sorted, string key)
        {
            if (sorted.ContainsKey(key))
            {
                for (var x = 0; x < sorted[key].Count; x++)
                {
                    var model = sorted[key];
                    var nestedModel = model[x].NestedModel;

                    if (nestedModel == null)
                    {
                        model[x].NestedModel = new Dictionary<string, CategoriesModel>();
                        model[x].NestedModel.Add(unsorted.Id, SortedModel(unsorted));
                    }
                    else
                    {
                        model[x].NestedModel.Add(unsorted.Id, SortedModel(unsorted));
                    }

                    nextEntryModel = NextEntry(model, x, unsorted);
                }
            }
            else
            {
                var firstLevelKey = FirstLevelKey(unsorted, sorted, key);
                FindNextNestedEntry(unsorted, sorted, key, FirstLevelKey(unsorted, sorted, key));
            }
        }

        private static void FindNextNestedEntry(CategoriesModel unsorted, IDictionary<string, List<CategoriesModel>> sorted, string key, string firstKey)
        {
            if (sorted.ContainsKey(firstKey))
            {
                var firstEntry = sorted[firstKey];
                SortDeepNested(unsorted, sorted, firstEntry, key);
            }
        }

        private static void SortDeepNested(CategoriesModel unsorted, IDictionary<string, List<CategoriesModel>> sorted, List<CategoriesModel> entry, string key)
        {
            for (var i = 0; i < entry.Count; i++)
            {
                if (entry[i].NestedModel.ContainsKey(key))
                {
                    SelectEntry(i, unsorted, entry, key);                    
                }
                else
                {
                    ThirdLevelEntry(i, unsorted, entry, sorted, key);
                }
            }
        }

        private static void SelectEntry(int i, CategoriesModel unsorted, List<CategoriesModel> entry, string key)
        {
            var nestedModel = entry[i].NestedModel[key].NestedModel;
            
            if (nestedModel == null)
            {
                entry[i].NestedModel[key].NestedModel = new Dictionary<string, CategoriesModel>();
                entry[i].NestedModel[key].NestedModel.Add(unsorted.Id, SortedModel(unsorted));
            }
            else
            {
                entry[i].NestedModel[key].NestedModel.Add(unsorted.Id, SortedModel(unsorted));
            }
        }

        private static void ThirdLevelEntry(int i, CategoriesModel unsorted, List<CategoriesModel> entry, IDictionary<string, List<CategoriesModel>> sorted, string key)
        {
            nextEntryModel = unsorted;
            var firstLevelKey = FirstLevelKey(unsorted, sorted, key);
            var secondLevelKey = SecondLevelKey(firstLevelKey, key, sorted);

            var model = entry[i].NestedModel[secondLevelKey].NestedModel.ContainsKey(key);
            var nestedModelCount = entry[i].NestedModel[secondLevelKey].NestedModel[key].NestedModel;

            if (model && nestedModelCount == null)
            {
                entry[i].NestedModel[secondLevelKey].NestedModel[key].NestedModel = new Dictionary<string, CategoriesModel>();
                entry[i].NestedModel[secondLevelKey].NestedModel[key].NestedModel.Add(unsorted.Id, SortedModel(unsorted));
            }
            else
            {
                entry[i].NestedModel[secondLevelKey].NestedModel[key].NestedModel.Add(unsorted.Id, SortedModel(unsorted));
            }
        }

        private static string SecondLevelKey(string firstLevelKey, string key, IDictionary<string, List<CategoriesModel>> sorted)
        {
            for (var i = 0; i < sorted.Count; i++)
            {
                var secondLevel = sorted[firstLevelKey][i].NestedModel;
                return SecondSelect(secondLevel, key);
            }

            return "Something Went Wrong?";
        }

        private static string SecondSelect(IDictionary<string, CategoriesModel> nestedModel, string key)
        {
            string newKey = "";
            int keyAsInteger;
            keyAsInteger = Int16.Parse(key);

            for (var i = 0; i < keyAsInteger; i++)
            {
                keyAsInteger--;
                newKey = keyAsInteger.ToString();

                if (nestedModel.ContainsKey(newKey))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            return newKey;
        }

        private static string FirstLevelKey(CategoriesModel unsorted, IDictionary<string, List<CategoriesModel>> sorted, string key)
        {
            if (nextEntryModel != null)
            {
                if (!sorted.ContainsKey(key))
                {
                    var select = Select(sorted, key);
                    return select;
                }
                else
                {
                    Console.WriteLine();
                }
            }

            return null;
        }

        private static string Select(IDictionary<string, List<CategoriesModel>> sorted, string key) 
        {
            string newKey = "";
            int keyAsInteger;
            keyAsInteger = Int16.Parse(key);

            for (var i = 0; i < keyAsInteger; i++)
            {
                keyAsInteger--;
                newKey = keyAsInteger.ToString();

                if (sorted.ContainsKey(newKey))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            return newKey;
        }

        private static CategoriesModel NextEntry(List<CategoriesModel> model, int x, CategoriesModel unsorted) 
        {
            if (model[x].NestedModel.ContainsKey(unsorted.Id))
            {
                return model[x].NestedModel[unsorted.Id];
            }

            return null;
        }

        private static CategoriesModel SortedModel(CategoriesModel unsorted)
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