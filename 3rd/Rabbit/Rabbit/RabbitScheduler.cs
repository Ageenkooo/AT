using Quartz;
using Quartz.Impl;

namespace Rabbit
{
    public class RabbitScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<RabbitSender>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("rabbitTrigger", "rabbitGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
