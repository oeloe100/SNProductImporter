using SNPIDataManager.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public static class FeedPathHelper
    {
        public static string Path()
        {
            var currentDate = DateTime.Now;
            var shortDate = currentDate.Date.ToShortDateString();

            return LocationsConfig.ReadLocations("localhostFeedDownloadLocation") + "EDCFeed" + shortDate + ".xml";
        }
    }
}