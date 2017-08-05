using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Api.Quartz
{
    public class JobScheduler

    {

        public static void Start()
        {

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();
            if (!scheduler.IsStarted)
                scheduler.Start();
            // Define the Job to be scheduled
            var job = JobBuilder.Create<EmailJob>()
                .WithIdentity("JobMonthSchedulerSeventhDay", "IT")
                .RequestRecovery()
                .Build();
            var job2 = JobBuilder.Create<SendMailBirthDayJob>()
                .WithIdentity("JobSendMailBirthDay", "IT")
                .RequestRecovery()
                .Build();




            ITrigger trigger = TriggerBuilder.Create()

                .WithDailyTimeIntervalSchedule

                  (s =>

                     s.WithIntervalInHours(24)

                    .OnEveryDay()

                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))

                  )

                .Build();

            ITrigger trigger2 = TriggerBuilder.Create()

                .WithDailyTimeIntervalSchedule

                  (s =>

                     s.WithIntervalInHours(2)

                    .OnEveryDay()

                   .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))

                  )

                .Build();
            // Validate that the job doesn't already exists
            if (scheduler.CheckExists(new JobKey("JobMonthSchedulerSeventhDay", "IT")))
            {
                scheduler.DeleteJob(new JobKey("JobMonthSchedulerSeventhDay", "IT"));
            }
            if (scheduler.CheckExists(new JobKey("JobSendMailBirthDay", "IT")))
            {
                scheduler.DeleteJob(new JobKey("JobSendMailBirthDay", "IT"));
            }
            scheduler.ScheduleJob(job, trigger);

            scheduler.ScheduleJob(job2, trigger2);

        }

    }

}