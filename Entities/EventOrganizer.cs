using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class EventOrganizer : BaseEntity
    {
        public int EventId { get; set; }
        public Events Event { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
    }
}
