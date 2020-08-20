using Quartz;
using Quartz.Impl;
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
        /// TaskManager. Executed at application start and executes configured jobs as implemented.
        /// </summary>
        public static async void Start()
        {
            //LogProvider.SetCurrentLogProvider(new CustomLogProvider());

            //Instantiate MAIN scheduler. and run jobs as scheduled.
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            //Create job (FeedDownload)
            IJobDetail job = JobBuilder.Create<TestJob>().Build();

            //Create Triggers to trigger all or certain jobs at specific time or date etc..
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s => s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                ).Build();

            //Schedule jobs. self explanatory.
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}