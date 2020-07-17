using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class SupplierCategoryBuilder : SupplierFeedHelper
    {
        public readonly IDictionary<string, List<SupplierModel>> RelationToView;
        private readonly List<SupplierModel> _SupplierModel;
        private readonly XmlElement _Root;

        public SupplierCategoryBuilder(XmlElement root, string feedPath)
        {
            RelationToView = new Dictionary<string, List<SupplierModel>>();
            _SupplierModel = new List<SupplierModel>();
            _Root = root;

            BuildCategories();
            BuildCategoryRelation();
        }

        private void BuildCategories()
        {
            var startNode = SelectCategoryNodes(_Root);
            var containsModel = false;
            var parentId = "";
            var parentTitle = "";

            for (var i = 0; i < startNode.Count(); i++)
            {
                for (var x = 0; x < startNode[i].ChildNodes.Count; x++)
                {
                    var deepNestedChildNode = startNode[i].ChildNodes[x];

                    if (startNode[i].ChildNodes[x - 1] != null)
                    {
                        parentId = startNode[i].ChildNodes[x - 1].ChildNodes[0].InnerText;
                        parentTitle = startNode[i].ChildNodes[x - 1].ChildNodes[1].InnerText;
                    }

                    var supplierModel = new SupplierModel()
                    {
                        RootId = deepNestedChildNode.ChildNodes[0].InnerText,
                        RootTitle = deepNestedChildNode.ChildNodes[1].InnerText,
                        Layer = x,
                        ChildId = parentId,
                        ChildTitle = parentTitle
                    };

                    containsModel = _SupplierModel.Where<SupplierModel>
                        (index => index.RootId == supplierModel.RootId && 
                        index.RootTitle == supplierModel.RootTitle).Count() > 0;

                    AddProductCount(ref supplierModel, supplierModel);

                    if (!containsModel)
                        _SupplierModel.Add(supplierModel);
                }
            }
        }

        private void BuildCategoryRelation()
        {
            for (var i = 0; i < _SupplierModel.Count; i++)
            {
                switch (_SupplierModel[i].Layer)
                {
                    case 0:
                        RelationToView.Add(
                            _SupplierModel[i].RootTitle, 
                            new List<SupplierModel>());
                        break;
                    case 1:
                        RelationToView[_SupplierModel[i].ChildTitle].Add(
                            _SupplierModel[i]);
                        break;
                }
            }
        }

        private void AddProductCount(ref SupplierModel supplierModel, SupplierModel model)
        {
            var existingModel = _SupplierModel.Where<SupplierModel>
                    (index => index.RootId == model.RootId && index.RootTitle == model.RootTitle).ToList();

            switch (existingModel.Count)
            {
                case 0:
                    supplierModel.ProductCount++;
                    break;
                case 1:
                    existingModel[0].ProductCount++;
                    break;
            }
        }
    }
}