using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Admin Admin { get; set; }
        public Attendee Attendee { get; set; }
        public Organizer Organizer { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
