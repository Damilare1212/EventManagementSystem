using EventApp.Context;
using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Enums;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Implementations.Repositories
{
    public class EventRepository : BaseRepository<Events>, IEventRepository
    {
        public EventRepository(ApplicationContext context)
        {
            _context = context;
        }
      
        public bool Exist(int id)
        {
            return _context.Events.Include(a => a.EventOrganizers).ThenInclude(b => b.Organizer)
                  .Include(c => c.EventCategories).ThenInclude(c => c.Category).Include(p => p.EventAttendees)
                  .Where(f => f.IsDeleted == false).Any(h => h.Id == id);
        }

        public bool Exist(Expression<Func<Events, bool>> expression)
        {
            return _context.Events.Include(d => d.EventOrganizers).ThenInclude(b => b.Organizer)
                    .Include(s => s.EventCategories).ThenInclude(s => s.Category)
                    .Where(d => d.IsDeleted == false).Any(expression);
        }

        public async Task<Events> Get(Expression<Func<Events, bool>> expression)
        {
            var events =  await _context.Events.Include(b => b.EventOrganizers).ThenInclude(c => c.Organizer)
                .Include(h => h.EventCategories).ThenInclude(h => h.Category).Include(d => d.EventAttendees)
                .Where(c => c.IsDeleted == false).FirstOrDefaultAsync(expression);
            return events;
        }

        public async Task<IList<Events>> GetAll()
        {
            return await _context.Events.Include(h => h.EventOrganizers).ThenInclude(k => k.Organizer)
                .Include(p => p.EventCategories).ThenInclude(f => f.Category).Include(d => d.EventAttendees)
                .ThenInclude(n => n.Attendee).Where(s => s.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Events>> GetAll(Expression<Func<Events, bool>> expression)
        {
            return await _context.Events.Include(g => g.EventOrganizers).ThenInclude(k => k.Organizer)
                .Include(l => l.EventCategories).ThenInclude(f => f.Category).Include(k => k.EventAttendees)
                .ThenInclude(r => r.Attendee).Where(d => d.IsDeleted == false ).ToListAsync();
        }

        public async Task<IList<Events>> GetAllAttendeePastEvents(int attendeeId)
        {
            return await _context.EventAttendees.Include(p => p.Event).Include(n => n.Attendee)
                .Where(events => events.IsDeleted == false && events.Event.EventDate < DateTime.UtcNow
                && events.AttendeeId == attendeeId).Select(events => events.Event).ToListAsync();
        }

        public async Task<IList<Events>> GetAllAttendeeUpcomingEvents(int attendeeId)
        {
            return await _context.EventAttendees.Include(events => events.Attendee)
                .Include(events => events.Event).Where(events => events.IsDeleted == false && 
                events.Event.EventDate > DateTime.UtcNow  && events.AttendeeId == attendeeId)
                .Select(events => events.Event).ToListAsync();
        }

        public async Task<IList<Events>> GetAllBookedEvents(int attendeeId)
        {
            return await _context.EventAttendees.Include(events => events.Attendee)
                .Include(j => j.Event).Where(h => h.IsDeleted == false && h.BookingStatus == BookingStatus.booked && 
                h.AttendeeId == attendeeId).Select(e => e.Event).ToListAsync();
        }

        public async Task<IList<Events>> GetAllNotPrivateEvents()
        {
            return await _context.Events.Include(j => j.EventOrganizers).ThenInclude(l => l.Organizer)
                .Include(b => b.EventCategories).ThenInclude(n => n.Category).Include(p => p.EventAttendees)
                .ThenInclude(k => k.Attendee).Where(c => c.IsDeleted == false && c.EventType != EventType.PrivateEvent 
                && c.EventDate >= DateTime.UtcNow).ToListAsync();
        }

        public async Task<IList<Events>> GetAllOrganizerPastEvents(int organizerId)
        {
            return await _context.EventOrganizers.Include(events => events.Organizer)
                .Include(events => events.Event)
                .Where(events => events.Event.IsDeleted == false && events.Event.EventDate < DateTime.UtcNow 
                && events.OrganizerId == organizerId)
                .Select(events => events.Event).ToListAsync();
        }

        public async Task<IList<Events>> GetSelected(IList<int> ids)
        {
            return await _context.Events.Include(a => a.EventOrganizers).ThenInclude(b => b.Organizer)
                .Include(d => d.EventCategories).ThenInclude(d =>d.Category).Include(f => f.EventAttendees)
                .Where(g => g.IsDeleted == false).Where(h => ids.Contains(h.Id)).ToListAsync();
        }

        public async Task<IList<Events>> GetSelected(Expression<Func<Events, bool>> expression)
        {
            return await _context.Events.Include(b => b.EventOrganizers).ThenInclude(c => c.Organizer)
                        .Include(d => d.EventCategories).ThenInclude(d => d.Category).Include(f => f.EventAttendees)
                        .Where(g => g.IsDeleted == false).Where(expression).ToListAsync();
        }

        public async Task<IList<Events>> GetAllOrganizerUpComingEvents(int organizerId)
        {
            return await _context.EventOrganizers.Include(events => events.Organizer).Include(events => events.Event)
                .Where(events => events.Event.IsDeleted == false && events.Event.EventDate > DateTime.UtcNow
                && events.OrganizerId ==  organizerId).Select(events => events.Event).ToListAsync();
        }

        public async Task<IList<EventDto>> Search(string searchText)
        {
            return await _context.Events.Include(b => b.EventOrganizers).ThenInclude(c => c.Organizer).Include(d => d.EventCategories).
                     ThenInclude(c => c.Category).Include(b => b.EventAttendees).ThenInclude(c => c.Attendee)
                  .Where(Event => EF.Functions.Like(Event.Title, $"%{searchText}%")  && Event.EventType == Enums.EventType.FreeEvent && Event.EventType == Enums.EventType.PayEvent
                   && Event.EventDate > DateTime.UtcNow)
             .Select(f => new EventDto
             {

                 EventCategories = f.EventCategories.Select(c => new CategoryDto
                 {
                     Id = c.CategoryId,
                     Name = c.Category.Name

                 }).ToList(),
                 EventDate = f.EventDate,
                 EventOrganizers = f.EventOrganizers.Select(e => new OrganizerDto
                 {
                     Id = e.OrganizerId,
                     FirstName = e.Organizer.FirstName,
                     LastName = e.Organizer.LastName

                 }).ToList(),
                 EventTime = f.EventTime,
                 Theme = f.Theme,
                 Title = f.Title,
                 Venue = f.Venue

             }).ToListAsync();
        }

        public async Task<EventAttendee> CancelEvent(int eventsId)
        {
            return await _context.EventAttendees.Include(events => events.Attendee).Include(events => events.Event)
                .Where(events => events.Event.IsDeleted == false && events.BookingStatus == BookingStatus.cancelled
                && events.EventId == eventsId).SingleOrDefaultAsync();
        }

        public async Task<EventAttendee> GetAttendeeEventById(int attendeeId, int eventsId)
        {
            var attendeeEvent = await _context.EventAttendees.FirstOrDefaultAsync(events => events.IsDeleted == false
           && events.AttendeeId == attendeeId && events.EventId == eventsId);
            return attendeeEvent;
        }

        public async Task<IList<Events>> GetOrganizerAllEvents(int organizerId)
        {
            return await _context.EventOrganizers.Include(eventOrganizer => eventOrganizer.Organizer)
                .Include(eventOrganizer => eventOrganizer.Event)
                .Where(events => events.IsDeleted == false && events.OrganizerId == organizerId)
                .Select(events => events.Event).ToListAsync();
        }
    }
}
