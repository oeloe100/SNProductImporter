using Quartz;
using SNPIDataManager.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Jobs
{
    public class FeedDownloadingJob : IJob
    {
        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger("FileAppender");

        /// <summary>
        /// Execution of selected Job. Download Feed from EDCFeed URL to LocalFolder.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            var webClient = new WebClient();
            var fullFeedUrl = new System.Uri("http://api.edc.nl/b2b_feed.php?key=4500c66ct0e0w63c8r4129tc80e622rr&sort=xml&type=xml&lang=nl&version=2015");

            var currentDate = DateTime.Now;
            var simpleDate = currentDate.Date.ToShortDateString();

            var test = LocationsConfig.ReadLocations("localhostFeedDownloadLocationFixed");

            try
            {
                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadFile(fullFeedUrl, LocationsConfig.ReadLocations("localhostFeedDownloadLocationFixed") + "\\EDCFeed" + simpleDate + ".xml");

                _Logger.Debug("(EDC) feed is successfully downloaded...");
                await Console.Out.WriteLineAsync(".");
            }
            catch (Exception ex)
            {
                _Logger.Error("error while downloading feed: ", ex);
            }
        }
    }
}