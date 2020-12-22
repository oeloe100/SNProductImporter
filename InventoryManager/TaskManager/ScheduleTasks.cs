using InventoryManager.Jobs;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace InventoryManager.TaskManager
{
    public class ScheduleTasks
    {
        public static async Task ProductUpdateTask()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            JobKey downloadFeedJobKey = JobKey.Create("downloadFeedJob", "pipeline");
            IJobDetail downloadFeedJob = JobBuilder.Create<DownloadFeedJob>().WithIdentity(downloadFeedJobKey).Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger", "pipeline")
                .WithDailyTimeIntervalSchedule
                (s => s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(16, 11))
                ).Build();

            //JobChainingJobListener listener = new JobChainingJobListener("pipeline chain");
            //scheduler.ListenerManager.AddJobListener(listener, GroupMatcher<JobKey>.GroupEquals("pipline"));

            await scheduler.ScheduleJob(downloadFeedJob, trigger);
        }
    }
}
