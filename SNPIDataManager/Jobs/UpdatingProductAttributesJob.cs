using Quartz;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Jobs
{
    public class UpdatingProductAttributesJob : IJob
    {
        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger("FileAppender");

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await RelationsHelper.UpdateProductAttributesScheduled();
                _Logger.Debug("Updating Product Attributes job executed successfully...");
            }
            catch (Exception ex)
            {
                _Logger.Error("error while executing testjob: ", ex);
            }
        }
    }
}