using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.SendMail
{
    public static class SendEmail
    {
        public static int Send(
            string fromEmail,
            string password,
            IEnumerable<string> toEmails,
            IEnumerable<string> toCcEmails,
            string subject,
            string body,
            bool isBodyHtml)
        {
            int result;
            if (string.IsNullOrEmpty(fromEmail))
                return 10;
            if (string.IsNullOrEmpty(password))
                return 20;
            var toSendEmails = toEmails as string[] ?? toEmails.ToArray();
            if (toEmails == null || !toSendEmails.Any())
                return 30;

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                //Port = 465,
                //Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, password)
            };

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmail);
                message.Subject = subject ?? "";
                message.Body = body ?? "";
                message.IsBodyHtml = isBodyHtml;
                foreach (var email in toSendEmails)
                {
                    //TODO: Check email is valid
                    message.To.Add(email);
                }
                var ccEmails = toCcEmails as string[] ?? toCcEmails.ToArray();
                if (toCcEmails != null && ccEmails.Any())
                {
                    foreach (var emailCc in ccEmails)
                    {
                        //TODO: Check CC email is valid
                        message.CC.Add(emailCc);
                    }
                }

                try
                {
                    //Send Mail
                    smtpClient.Send(message);
                    result = -1;
                }
                catch (Exception ex)
                {
                    Common.Logs.LogCommon.WriteLogError(ex.Message);
                    result = 60;
                }
            }
            return result;
        }
    }
}
