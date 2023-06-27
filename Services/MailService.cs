using EventApp.Mail_Model;
using EventApp.Setting;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Services
{
    public class MailService : IMailServices
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileByte;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            file.CopyTo(memoryStream);
                            fileByte = memoryStream.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileByte, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(WelcomeRequest welcomeRequest)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader streamReader = new StreamReader(FilePath);
            string MailText = streamReader.ReadToEnd();
            streamReader.Close();
            MailText = MailText.Replace("[username]",
                $"{welcomeRequest.FirstName} {welcomeRequest.LastName}").Replace("[email]", welcomeRequest.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(welcomeRequest.ToEmail));
            email.Subject = $"Dear {welcomeRequest.UserName}, you are higly welcome to Auspicious Event. The platform that answer your question: How was the event?";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
          //  smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
           // smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);
        }

        public async Task SendUpcomingEventEmailAsync(UpcomingEventRequest upcomingEventRequest)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Teemplates\\UpcomingEvent.html";
            StreamReader streamReader = new StreamReader(FilePath);
            string MailText = streamReader.ReadToEnd();
            streamReader.Close();
            MailText = MailText.Replace("[Username]", $"{upcomingEventRequest.FirstName} {upcomingEventRequest.LastName}")
                .Replace("[email]", upcomingEventRequest.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(upcomingEventRequest.ToEmail));
            email.Subject = $"Dear {upcomingEventRequest.FirstName}, this is to notify you of upcoming event which might be interest you";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
