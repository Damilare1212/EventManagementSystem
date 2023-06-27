using EventApp.Entities;
using EventApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Venue { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string Instructions { get; set; }
        public EventType EventType { get; set; }
        public DateTime Created { get; set; }
        public ICollection<CategoryDto> EventCategories { get; set; } = new List<CategoryDto>();
        public IEnumerable<OrganizerDto> EventOrganizers { get; set; } = new List<OrganizerDto>();

    }
    public class CreateEventRequestModel
    {
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Venue { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public DateTime Created { get; set; }
        public EventType EventType { get; set; }
        public string Instructions { get; set; }
        public IList<int> CategoryIds { get; set; } = new List<int>();
        public IList<int> OrganizerIds { get; set; } = new List<int>();

    }
    public class UpdateEventRequestModel
    {
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Venue { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string Instructions { get; set; }
        public EventType EventType { get; set; }
        public IList<int> CategoryIds { get; set; } = new List<int>();
    }
}
