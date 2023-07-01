using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EventApp.Mail_Model
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<IFormFile> Attachments { get; set; }
    }
}
