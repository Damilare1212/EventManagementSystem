using EventApp.DTOs;
using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface IEventRepository : IRepository<Events> 
    {
        public bool Exist(int id);
        public bool Exist(Expression<Func<Events, bool>> expression);
        public Task<Events> Get(Expression<Func<Events, bool>> expression);
        public Task<IList<Events>> GetSelected(IList<int> ids);
        public Task<IList<Events>> GetAll();
        public Task<IList<Events>> GetAll(Expression<Func<Events, bool>> expression);
        public Task<IList<Events>> GetSelected(Expression<Func<Events, bool>> expression);
        public Task<IList<EventDto>> Search(string searchText);
        public Task<IList<Events>> GetAllNotPrivateEvents();
        public Task<IList<Events>> GetAllAttendeePastEvents(int attendeeId);
        public Task<IList<Events>> GetAllOrganizerPastEvents(int organizerId);
        public Task<IList<Events>> GetAllAttendeeUpcomingEvents(int attendeeId);
        public Task<IList<Events>> GetAllOrganizerUpComingEvents(int attendeId);
        public Task<IList<Events>> GetAllBookedEvents(int attendeeId);
        public Task<EventAttendee> GetAttendeeEventById(int attendeeId, int eventsId);
        public Task<IList<Events>> GetOrganizerAllEvents(int organizerId);

    }
}
