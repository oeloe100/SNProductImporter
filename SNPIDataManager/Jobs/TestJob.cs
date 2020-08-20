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
        private WebClient _WebClient;

        private string _PathToDwnldFolder;
        private Uri _FeedURL;
        private string _DateSimple;

        /// <summary>
        /// Change _PathToDwnldFolder for production. Currently Hardcoded path;
        /// </summary>
        private TestJob()
        {
            _WebClient = new WebClient();

            _PathToDwnldFolder = @"C:\Users\sexxnation\source\repos\Sexxnation\Product Importer\SNProductImporter\SNPIDataManager\FeedDownloads";
            _FeedURL = new System.Uri("http://api.edc.nl/b2b_feed.php?key=4500c66ct0e0w63c8r4129tc80e622rr&sort=xml&type=xml&lang=nl&version=2015");

            var currentDate = DateTime.Now;
            _DateSimple = currentDate.Date.ToShortDateString();
        }

        /// <summary>
        /// Execution of selected Job. Download Feed from EDCFeed URL to LocalFolder.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                _WebClient.DownloadFile(_FeedURL, _PathToDwnldFolder + "\\EDCFeed" + _DateSimple + ".xml");
                
                await Console.Out.WriteLineAsync("Done");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
            }
        }
    }
}