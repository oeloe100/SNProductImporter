using SNPIDataManager.Areas.EDCFeed.Models.ProductSpecificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public class SupplierFeedHelper
    {

        private IDictionary<string, List<string>> _ProductSpecAttributesWithValues = new Dictionary<string, List<string>>();

        internal List<XmlNode> SelectCategoryNodes(XmlElement _Root)
        {
            XmlNodeList productNodes = _Root.SelectNodes("//product");
            List<XmlNode> nodeList = new List<XmlNode>();

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
                                    nodeList.Add(categoriesChildNodes[n].ChildNodes[x]);
                                }
                            }
                        }
                    }
                }
            }

            return nodeList;
        }

        internal IEnumerable<XElement> SelectProductNodesByCategory(string feedPath, string mappedCategoryId)
        {
            XElement products = XElement.Load(feedPath);

            var productByCategoryQuery = from product in products.Elements("product")
                                         where (string)product.Element("categories").Element("category")
                                         .Elements("cat").ElementAt(1).Element("id") == mappedCategoryId
                                         select product;

            return productByCategoryQuery;
        }

        /********* ... *********/

        internal IDictionary<string, List<string>> RetrieveProductSpecificationAttributes(string feedPath)
        {
            XElement products = XElement.Load(feedPath);

            foreach (var product in products.Elements("product"))
            {
                var list = product.Element("properties").Elements("prop").Elements("property").ToList();

                for (var i = 0; i < list.Count; i++)
                {
                    var index = list[i].ToString();

                    if (!_ProductSpecAttributesWithValues.ContainsKey((string)list[i]))
                        _ProductSpecAttributesWithValues.Add((string)list[i],
                            RetrieveSpecificationAttributeValues(product.Element("properties").Elements("prop").ToList(), i));
                    else
                        foreach (var item in RetrieveSpecificationAttributeValues(product.Element("properties").Elements("prop").ToList(), i))
                            if (!_ProductSpecAttributesWithValues[(string)list[i]].Contains(item))
                                _ProductSpecAttributesWithValues[(string)list[i]].Add(item);
                }
            }

            return _ProductSpecAttributesWithValues;
        }

        private List<string> RetrieveSpecificationAttributeValues(List<XElement> property, int index)
        {
            List<string> attributeValues = new List<string>();

            var propertyValue = property[index].Element("values").Elements("value").ToList();

            ElementValuesByTitle(propertyValue, attributeValues);
            ElementValuesByUnits(propertyValue, attributeValues);

            return attributeValues;
        }

        private void ElementValuesByTitle(List<XElement> propertyValue, List<string> attributeValues)
        {
            var elementValues = propertyValue.Elements("title").ToList();

            for (var x = 0; x < elementValues.Count; x++)
            {
                var value = (string)elementValues[x];
                attributeValues.Add(value);
            }
        }

        private void ElementValuesByUnits(List<XElement> propertyValue, List<string> attributeValues)
        {
            var elementMagnitudeValue = propertyValue.Elements("magnitude").ToList();
            var elementUnitValue = propertyValue.Elements("unit").ToList();

            for (var y = 0; y < elementMagnitudeValue.Count; y++)
            {
                for (var z = 0; z < elementUnitValue.Count; z++)
                {
                    var value = (string)elementMagnitudeValue[y] + (string)elementUnitValue[z];
                    attributeValues.Add(value);
                }
            }
        }
    }
}
 