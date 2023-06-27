using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class Admin : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AdminPhoto { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
