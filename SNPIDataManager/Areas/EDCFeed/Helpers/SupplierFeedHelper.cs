﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public class SupplierFeedHelper
    {
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
    }
}