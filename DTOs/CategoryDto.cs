using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<EventDto> Event{ get; set; } = new List<EventDto>();
        public ICollection<AttendeeCategory> AttendeeCategories { get; set; } = new List<AttendeeCategory>();

    }
    public class CreateCategoryRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
