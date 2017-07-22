﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace RealEstate.Api.Quartz
{
    public class EmailJob : IJob

    {

        public void Execute(IJobExecutionContext context)

        {

            using (var message = new MailMessage("tranhoangnam11373@gmail.com", "tranhoangnam11373@gmail.com"))

            {

                message.Subject = "Test";

                message.Body = "Test at " + DateTime.Now;

                using (SmtpClient client = new SmtpClient

                {

                    EnableSsl = true,

                    Host = "smtp.gmail.com",

                    Port = 587,

                    Credentials = new NetworkCredential("tranhoangnam11373@gmail.com", "TRANHOANGNAM")

                })

                {

                    client.Send(message);

                }

            }

        }

    }

}