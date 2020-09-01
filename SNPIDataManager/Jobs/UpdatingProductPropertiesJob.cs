using Quartz;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Jobs
{
    public class UpdatingProductPropertiesJob : IJob
    {
        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger("FileAppender");

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await RelationsHelper.UpdateProductPropertiesScheduled();
                _Logger.Debug("Updating Product properties. Job executed successfully...");
            }
            catch (Exception ex)
            {
                _Logger.Error("Error while executing Product properties update Job: ", ex);
            }
        }
    }
}