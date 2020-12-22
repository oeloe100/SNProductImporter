using InventoryManager.Models;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Net;
using System.Threading.Tasks;
using DataLibrary.Select;
using Microsoft.Extensions.Logging;

namespace InventoryManager.Jobs
{
    [DisallowConcurrentExecution]
    public class DownloadFeedJob : IJob
    {
        private readonly IOptions<EDCDataPoco> _iOptions;
        private static ILogger<DownloadFeedJob> _logger;

        public DownloadFeedJob(
            IOptions<EDCDataPoco> iOptions, 
            ILogger<DownloadFeedJob> logger)
        {
            _iOptions = iOptions;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var webClient = new WebClient();
            var feedUrl = new System.Uri(_iOptions.Value.FeedUrl);

            var date = DateTime.UtcNow;
            var simpleDate = date.Date.ToShortDateString();

            try
            {
                webClient.DownloadFile(feedUrl, "EDCFeed-" + simpleDate + Extensions.FileExtensions.xml + "");
                _logger.LogWarning("(EDC) feed has been successfully downloaded...");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while downloading feed: ", ex);
            }

            throw new NotImplementedException();
        }
    }
}
