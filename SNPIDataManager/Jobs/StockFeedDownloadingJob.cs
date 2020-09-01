using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Jobs
{
    public class StockFeedDownloadingJob : IJob
    {
        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger("FileAppender");

        public async Task Execute(IJobExecutionContext context)
        {
            var webClient = new WebClient();

            var pathToDwnldFolder = @"C:\Users\sexxnation\source\repos\Sexxnation\Product Importer\SNProductImporter\SNPIDataManager\StockFeedDownload";
            var fullFeedUrl = new System.Uri("http://api.edc.nl/xml/eg_xml_feed_stock.xml");
            
            var currentDate = DateTime.Now;

            try
            {
                webClient.DownloadFileAsync(fullFeedUrl, pathToDwnldFolder + "\\EDCStockFeed" + currentDate + ".xml");

                _Logger.Debug("(EDC) Stock feed is successfully downloaded...");
                await Console.Out.WriteLineAsync(".");
            }
            catch (Exception ex)
            {
                _Logger.Error("error while downloading stock feed: ", ex);
            }
        }
    }
}