using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Listener;
using Quartz.Logging;
using SNPIDataManager.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.TaskManager
{
    public static class TaskManager
    {
        /// <summary>
        /// Scheduled jobs are added to a pipline and executed in order defined by jobchainingjoblistener.
        /// </summary>
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            JobKey feedDownloadJobKey = JobKey.Create("feedDownloadJob", "pipline");
            JobKey updatingProductAttributesJobKey = JobKey.Create("updatingProductAttributesJob", "pipline");

            IJobDetail feedDownloadJob = JobBuilder.Create<FeedDownloadingJob>().WithIdentity(feedDownloadJobKey).Build();
            IJobDetail updatingProductAttributesJob = JobBuilder.Create<UpdatingProductAttributesJob>().WithIdentity(updatingProductAttributesJobKey).Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger", "pipline")
                .WithDailyTimeIntervalSchedule
                (s => s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(14, 30))
                ).Build();

            JobChainingJobListener listener = new JobChainingJobListener("pipeline chain");
            listener.AddJobChainLink(feedDownloadJobKey, updatingProductAttributesJobKey);

            scheduler.ListenerManager.AddJobListener(listener, GroupMatcher<JobKey>.GroupEquals("pipline"));

            await scheduler.ScheduleJob(feedDownloadJob, trigger);
            await scheduler.AddJob(updatingProductAttributesJob, false, true);
        }
    }
}