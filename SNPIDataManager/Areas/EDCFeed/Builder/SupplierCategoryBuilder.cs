using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class SupplierCategoryBuilder
    {
        public readonly IDictionary<string, List<SupplierModel>> _RelationToView;
        private readonly List<SupplierModel> _SupplierTestModel;

        private readonly XmlElement _Root;

        public SupplierCategoryBuilder(XmlElement root)
        {
            _Root = root;
            _RelationToView = new Dictionary<string, List<SupplierModel>>();
            _SupplierTestModel = new List<SupplierModel>();

            SelectCategoryNodes();
            BuildCategoryRelation();
        }

        private void SelectCategoryNodes()
        {
            XmlNodeList productNodes = _Root.SelectNodes("//product");

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
                                    var availableCategory = SaveAvailableCategories(categoriesChildNodes[n].ChildNodes[x]);
                                    if (availableCategory != null)
                                        _SupplierTestModel.Add(availableCategory);
                                }
                            }
                        }
                    }
                }
            }
        }

        private SupplierModel SaveAvailableCategories(XmlNode startNode)
        {
            bool containsModel = false;
            var parentId = "";
            var parentTitle = "";

            for (var z = 0; z < startNode.ChildNodes.Count; z++)
            {
                var deepNestedChildNode = startNode.ChildNodes[z];

                if (startNode.ChildNodes[z - 1] != null)
                { 
                    parentId = startNode.ChildNodes[z - 1].ChildNodes[0].InnerText;
                    parentTitle = startNode.ChildNodes[z - 1].ChildNodes[1].InnerText;
                }

                var testModel = new SupplierModel()
                {
                    Id = deepNestedChildNode.ChildNodes[0].InnerText,
                    Title = deepNestedChildNode.ChildNodes[1].InnerText,
                    Layer = z,
                    ParentId = parentId,
                    ParentTitle = parentTitle
                };

                containsModel = _SupplierTestModel.Where<SupplierModel>
                    (index => index.Id == testModel.Id && index.Title == testModel.Title).Count() > 0;

                if (!containsModel)
                    return testModel;
            }

            return null;
        }

        private void BuildCategoryRelation()
        {
            for (var i = 0; i < _SupplierTestModel.Count; i++)
            {
                if (_SupplierTestModel[i].Layer <= 0 && 
                    !_RelationToView.ContainsKey(_SupplierTestModel[i].Title))
                { 
                    _RelationToView.Add(_SupplierTestModel[i].Title, new List<SupplierModel>());
                }
                else if (_SupplierTestModel[i].Layer > 0 && 
                    _RelationToView.ContainsKey(_SupplierTestModel[i].ParentTitle))
                { 
                    _RelationToView[_SupplierTestModel[i].ParentTitle].Add(_SupplierTestModel[i]);
                }
            }
        }
    }
}