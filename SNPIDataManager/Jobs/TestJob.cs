using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Jobs
{
    public class TestJob : IJob
    {
        /// <summary>
        /// Execution of selected Job. Download Feed from EDCFeed URL to LocalFolder.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            var webClient = new WebClient();
            
            var pathToDwnldFolder = @"C:\Users\sexxnation\source\repos\Sexxnation\Product Importer\SNProductImporter\SNPIDataManager\FeedDownloads";
            var fullFeedUrl = new System.Uri("http://api.edc.nl/b2b_feed.php?key=4500c66ct0e0w63c8r4129tc80e622rr&sort=xml&type=xml&lang=nl&version=2015");

            var currentDate = DateTime.Now;
            var simpleDate = currentDate.Date.ToShortDateString();

            try
            {
                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadFile(fullFeedUrl, pathToDwnldFolder + "\\EDCFeed" + simpleDate + ".xml");
                
                await Console.Out.WriteLineAsync("Done");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
            }
        }
    }
}