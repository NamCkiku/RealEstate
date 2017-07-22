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



            IJobDetail job = JobBuilder.Create<EmailJob>().Build();



            ITrigger trigger = TriggerBuilder.Create()

                .WithDailyTimeIntervalSchedule

                  (s =>

                     s.WithIntervalInHours(24)

                    .OnEveryDay()

                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))

                  )

                .Build();



            scheduler.ScheduleJob(job, trigger);

        }

    }

}