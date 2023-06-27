using EventApp.Mail_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Services
{
    public interface IMailServices
    {
       Task SendEmailAsync(MailRequest mailRequest);
       Task SendWelcomeEmailAsync(WelcomeRequest welcomeRequest);
       Task SendUpcomingEventEmailAsync(UpcomingEventRequest upcomingEventRequest);

    }
}
