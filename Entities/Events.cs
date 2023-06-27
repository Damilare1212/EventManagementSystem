using EventApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class Events : BaseEntity
    {
        public string Title { get; set; }
        public string Theme { get; set; }                                                                     
        public string Venue { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string Instructions { get; set; }
        public DateTime Created { get; set; }
        public EventType EventType { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
        public ICollection<EventOrganizer> EventOrganizers { get; set; } = new List<EventOrganizer>();      
    }
    
}
