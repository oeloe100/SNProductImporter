﻿using Quartz;
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
        /// Scheduled jobs are added to the pipline and executed in order defined by jobchainingjoblistener.
        /// </summary>
        public static async void FullProductUpdateTask()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            JobKey feedDownloadJobKey = JobKey.Create("feedDownloadJob", "pipline");
            JobKey updatingProductAttributesJobKey = JobKey.Create("updatingProductAttributesJob", "pipline");

            IJobDetail feedDownloadJob = JobBuilder.Create<FeedDownloadingJob>().WithIdentity(feedDownloadJobKey).Build();
            IJobDetail updatingProductAttributesJob = JobBuilder.Create<UpdatingProductPropertiesJob>().WithIdentity(updatingProductAttributesJobKey).Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger", "pipline")
                .WithDailyTimeIntervalSchedule
                (s => s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 18))
                ).Build();

            JobChainingJobListener listener = new JobChainingJobListener("pipeline chain");
            listener.AddJobChainLink(feedDownloadJobKey, updatingProductAttributesJobKey);

            scheduler.ListenerManager.AddJobListener(listener, GroupMatcher<JobKey>.GroupEquals("pipline"));

            await scheduler.ScheduleJob(feedDownloadJob, trigger);
            await scheduler.AddJob(updatingProductAttributesJob, false, true);
        }

        public static async void ProductStockUpateTask()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            //JobKey stockFeedDownloadJobKey = JobKey.Create("stockFeedDownloadJob", "stock_pipeline");
            //JobKey updatingProductVariantStockJobKey = JobKey.Create("updatingProductVariantStockJob", "stock_pipeline");

            //IJobDetail stockFeedDownloadJob = JobBuilder.Create<StockFeedDownloadingJob>().WithIdentity(stockFeedDownloadJobKey).Build();
            //IJobDetail updatingProductVariantStockJob = JobBuilder.Create<UpdateProductStockJob>().WithIdentity(updatingProductVariantStockJobKey).Build();

            IJobDetail updatingProductStockJob = JobBuilder.Create<UpdateProductStockJob>()
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("stock_trigger", "stock_pipeline")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(updatingProductStockJob, trigger);

            //JobChainingJobListener listener = new JobChainingJobListener("stock_pipeline chain");
            //listener.AddJobChainLink(stockFeedDownloadJobKey, updatingProductVariantStockJobKey);

            //scheduler.ListenerManager.AddJobListener(listener, GroupMatcher<JobKey>.GroupEquals("stock_pipeline"));

            //await scheduler.ScheduleJob(stockFeedDownloadJob, trigger);
            //await scheduler.AddJob(updatingProductVariantStockJob, false, true);
        }
    }
}