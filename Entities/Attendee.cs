using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class Attendee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<AttendeeCategory> AttendeeCategories { get; set; } = new List<AttendeeCategory>();
    }
}
