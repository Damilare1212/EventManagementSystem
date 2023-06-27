using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class AttendeeCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; }
    }
}
