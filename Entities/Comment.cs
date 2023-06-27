using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class Comment : BaseEntity
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventsId { get; set; }
        public Events Events { get; set; }
    }
}
