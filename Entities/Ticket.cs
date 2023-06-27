using EventApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class Ticket : BaseEntity
    {
        public string TicketNumber { get; set; }
        public EventType EventType { get; set; }
        public int EventId { get; set; }
        public Events Event { get; set; }
        public int  AttendeeId { get; set; }
        public Attendee Attendee { get; set; }
    }
}
