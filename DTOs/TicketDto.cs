using EventApp.Entities;
using EventApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public int  EventId { get; set; }
        public int AttendeeId { get; set; }
        public EventType EventType { get; set; }
        public bool IsDeleted { get; set; }

        public class CreateTicketRequestModel
        {
            public EventType EventType { get; set; }
            public int EventId { get; set; }
            public int AttendeeId { get; set; }
        }

        public class UpdateTicketRequestModel
        {
            public int EventId { get; set; }
            public int AttendeeId { get; set; }
            public EventType EventType { get; set; }

        }
    }
}
