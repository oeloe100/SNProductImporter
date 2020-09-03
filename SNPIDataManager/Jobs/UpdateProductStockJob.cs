using Quartz;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Config;
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
                var stockFeedUrl = new System.Uri(LocationsConfig.ReadLocations("edcStockUpdateFeedUrl"));
                 await RelationsHelper.UpdateProductStockScheduled(stockFeedUrl);

                _Logger.Debug("Updating Product stock. Job executed successfully...");
            }
            catch (Exception ex)
            {
                _Logger.Error("Error while executing Product stock update Job: ", ex);
            }
        }
    }
}