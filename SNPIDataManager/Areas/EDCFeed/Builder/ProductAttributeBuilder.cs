using SNPIDataManager.Models.NopProductsModel;
using SNPIDataManager.Models.NopProductsModel.SyncModels;
using SNPIDataManager.Models.NopProductsModel.SyncUpdateModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class ProductAttributeBuilder
    {
        internal List<ProductSyncModelAttributes> SetAttribute(XElement element)
        {
            var attrList = new List<ProductSyncModelAttributes>();

            attrList.Add(new ProductSyncModelAttributes()
            {
                ProductAttributeId = 8,
                ProductAttributeName = "Size",
                TextPrompt = "",//(string)nodeList[i].Element("subartnr"),
                IsRequired = false,
                AttributeControlTypeId = 1,
                DisplayOrder = 0,
                DefaultValue = "",
                AttributeControlTypeName = "DropdownList",
                AttributeValues = SetAttributeValues(element),
            });

            return attrList;
        }

        internal List<ProductSyncModelAttributeValues> SetAttributeValues(XElement element)
        {
            var model = new List<ProductSyncModelAttributeValues>();

            foreach (var variant in element.Element("variants").Elements("variant"))
            {
                model.Add(new ProductSyncModelAttributeValues()
                {
                    TypeId = 0,
                    Name = CheckVariantNameProperty(
                        variant.Element("title"),
                        (string)element.Element("title")),
                    ColorSquaresRGB = "",
                    PriceAdjustment = 0,
                    WeightAdjustment = 0,
                    Cost = 0,
                    Quantity = CheckElementAvailabilityByInt(
                        variant.Element("stockestimate")),
                    IsPreSeleted = false,
                    DisplayOrder = 0,
                    ProductPictureId = 0,
                    Type = "Simple"
                });
            }

            return model;
        }

        internal List<ProductUpdateModelAttributes> UpdateAttribute(int productId, int attributeCount)
        {
            var attrList = new List<ProductUpdateModelAttributes>();

            attrList.Add(new ProductUpdateModelAttributes()
            {
                ProductAttributeId = 8,
                ProductAttributeName = "Size",
                IsRequired = true,
                AttributeControlTypeId = 1,
                DisplayOrder = 0,
                AttributeControlTypeName = "DropdownList",
                AttributeValues = UpdateAttributeValues(productId, attributeCount),
            });

            return attrList;
        }

        internal List<ProductUpdateModelAttributeValues> UpdateAttributeValues(int productId, int attributeCount)
        {
            var model = new List<ProductUpdateModelAttributeValues>();
            for (var i = 0; i < attributeCount; i++)
                model.Add(new ProductUpdateModelAttributeValues() { AssociatedProductId = productId });

            return model;
        }

        internal List<ProductSyncModelAttributeCombinations> SetAttributeCombinations(XElement element)
        {
            var model = new List<ProductSyncModelAttributeCombinations>();

            foreach (var variant in element.Element("variants").Elements("variant"))
            {
                model.Add(new ProductSyncModelAttributeCombinations()
                {
                    StockQuantity = CheckElementAvailabilityByInt(
                        variant.Element("stockestimate")),
                    Sku = (string)variant.Element("subartnr"),
                    Gtin = (string)variant.Element("ean")
                });
            }

            /*
            foreach (var obj in model)
            {
                foreach (var id in attrValuesId)
                {
                    obj.AttributesXml = "<Attributes><ProductAttribute ID=\"" + attrId + "\">" +
                    "<ProductAttributeValue><Value>" + id + "</Value></ProductAttributeValue></ProductAttribute></Attributes>";
                }
            }*/

            return model;
        }

        internal int CheckElementAvailabilityByInt(XElement element)
        {
            if (element != null)
                return (int)element;
            else
                return 0;
        }

        internal decimal CheckElementAvailabilityByDecimal(XElement element)
        {
            if (element != null)
                return (decimal)element;
            else
                return 0;
        }

        internal string CheckVariantNameProperty(XElement element, string optional)
        {
            if ((string)element == "" || element == null)
                if (!string.IsNullOrEmpty(optional))
                    return optional;

            return (string)element;
        }

        internal string ProductGtinByVariant(XElement element)
        {
            var variants = element.Element("variants").Elements("variant");

            if (variants.Count() <= 1)
                return (string)variants.First().Element("ean");
            else
                return (string)element.Element("artnr");
        }
    }
}