using Quartz;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Jobs
{
    public class UpdateProductStockJob : IJob
    {
        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger("FileAppender");

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var fullFeedUrl = new System.Uri("http://api.edc.nl/xml/eg_xml_feed_stock.xml");
                await RelationsHelper.UpdateProductStockScheduled(fullFeedUrl);

                _Logger.Debug("Updating Product stock. Job executed successfully...");
            }
            catch (Exception ex)
            {
                _Logger.Error("Error while executing Product stock update Job: ", ex);
            }
        }
    }
}