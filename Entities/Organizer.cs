using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class Organizer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public IEnumerable<EventOrganizer> EventOrganizers { get; set; } = new List<EventOrganizer>();
    }
}
