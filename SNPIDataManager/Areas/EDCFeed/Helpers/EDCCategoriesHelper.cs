using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using WebGrease.Css.Ast.Selectors;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public static class EDCCategoriesHelper
    {
        public static IEnumerable<CategoryModelList> CategorizeNodes(XmlElement root)
        {
            List<CategoryModelList> CategoriesParented = new List<CategoryModelList>();

            XmlNodeList productNodes = root.SelectNodes("//product");
            for (var i = 0; i < productNodes.Count; i++)
            {
                if (productNodes.Count > 0)
                {
                    for (var n = 0; n < productNodes[i].ChildNodes.Count; n++)
                    {
                        XmlNodeList categoriesChildNodes = productNodes[i].ChildNodes;
                        if (categoriesChildNodes[n].Name == "categories")
                        {
                            for (int x = 0; x < categoriesChildNodes[n].ChildNodes.Count; x++)
                            {
                                if (categoriesChildNodes[n].ChildNodes[x].Name == "category")
                                {
                                    List<CategoryModel> CategoryModelList = new List<CategoryModel>();
                                    List<bool> CheckEveryEntry = new List<bool>();

                                    bool isDuplicate = true;

                                    /*** Fill Category Model with Data From 'EDC XML FEED' per Product ***/
                                    for (var z = 0; z < categoriesChildNodes[n].ChildNodes[x].ChildNodes.Count; z++)
                                    {
                                        var categoryModel = new CategoryModel()
                                        {
                                            Id = categoriesChildNodes[n].ChildNodes[x].ChildNodes[z].ChildNodes[0].InnerText,
                                            Title = categoriesChildNodes[n].ChildNodes[x].ChildNodes[z].ChildNodes[1].InnerText
                                        };

                                        CategoryModelList.Add(categoryModel);
                                        CheckEveryEntry.Add(IsDuplicate(CategoriesParented, categoryModel.Title));
                                    }

                                    /*** Check if every category entry in CheckEveryEntry(Bool List) = false (duplicate). 
                                        * If not we Can Savely add Category (May Be Duplicate) With SubCategory (Unique) ***/
                                    if (CheckEveryEntry.Count == categoriesChildNodes[n].ChildNodes[x].ChildNodes.Count)
                                    {
                                        for (var v = 0; v < CheckEveryEntry.Count; v++)
                                        {
                                            if (CheckEveryEntry[v] == false)
                                            {
                                                isDuplicate = false;
                                            }
                                        }
                                    }

                                    /*** Create Model Without duplicated SUB CATEGORY Items And return to View. ***/
                                    if (!isDuplicate)
                                    {
                                        CreateFinalCategoriesModel(CategoriesParented, CategoryModelList);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return CategoriesParented;
        }

        public static void ChildParentRelationForView(List<CategoryModelList> ParentedCategories, IDictionary<string, List<CategoryModel>> dict)
        {
            foreach (var model in ParentedCategories)
            {
                if (!dict.ContainsKey(model.CategoryModel[0].Title))
                {
                    dict[model.CategoryModel[0].Title] = new List<CategoryModel>();
                }

                dict[model.CategoryModel[0].Title].Add(SetCategoryModel
                    (
                        model.CategoryModel[1].Title, 
                        model.CategoryModel[1].Id)
                    );
            }
        }

        public static CategoryModel SetCategoryModel(string title, string id)
        {
            return new CategoryModel()
            {
                Title = title,
                Id = id
            };
        }

        private static bool IsDuplicate(List<CategoryModelList> CategoriesParented, string title)
        {
            var item = CategoriesParented.Select(model => model.CategoryModel.Select(index => index.Title)).ToList();

            if (item.Count >= 0)
            {
                for (var i = 0; i < item.Count; i++)
                {
                    for (var x = 0; x < item[i].Count(); x++)
                    {
                        var obj = item[i].ElementAt(x);
                        if (obj == title)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static void CreateFinalCategoriesModel(List<CategoryModelList> CategoriesParented, List<CategoryModel> categoryModel)
        {
            var model = new CategoryModelList()
            {
                CategoryModel = categoryModel
            };

            CategoriesParented.Add(model);
        }
    }
}
 
 