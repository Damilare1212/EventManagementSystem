using EventApp.Context;
using EventApp.Entities;
using EventApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Implementations.Repositories
{
    public class EventAttendeeRepository : BaseRepository<EventAttendee>, IEventAttendeeRepository
    {
        public EventAttendeeRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}
