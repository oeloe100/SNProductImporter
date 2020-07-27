﻿using SNPIDataManager.Models.NopProductsModel.SyncModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class ProductImageBuilder
    {
        private string _ImageSource;

        public ProductImageBuilder()
        {
            _ImageSource = "http://cdn.edc-internet.nl/500/";
        }

        internal List<ProductSyncModelImages> SelectImageProperties(XElement element)
        {
            var model = new List<ProductSyncModelImages>();

            foreach (var pic in element.Element("pics").Elements("pic"))
            {
                model.Add(new ProductSyncModelImages()
                {
                    Src = _ImageSource + (string)pic,
                    Attachment = "",
                });
            }

            return model;
        }
    }
}