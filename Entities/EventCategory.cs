using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Entities
{
    public class EventCategory : BaseEntity
    {
        public int EventId { get; set; }
        public Events Event { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
