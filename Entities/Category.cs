using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
        public ICollection<AttendeeCategory> CategoryAttendees { get; set; } = new List<AttendeeCategory>();
    }
}
