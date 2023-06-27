using EventApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class EventAttendee : BaseEntity
    {
        public int EventId { get; set; }
        public Events Event { get; set; }
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } 
        public BookingStatus BookingStatus { get; set; }

    }
}
