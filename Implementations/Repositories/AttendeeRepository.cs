using EventApp.Context;
using EventApp.Entities;
using EventApp.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Implementations.Repositories
{
    public class AttendeeRepository : BaseRepository<Attendee>, IAttendeeRepository
    {
        public AttendeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Exist(int id)
        {
            return _context.Attendees.Include(t => t.User).ThenInclude(j => j.UserRoles)
                .ThenInclude(p => p.Role).Where(g => g.IsDeleted == false).Any(p => p.Id == id);
        }

        public bool Exists(Expression<Func<Attendee, bool>> expression)
        {
            return _context.Attendees.Include(j => j.User).ThenInclude(s => s.UserRoles).ThenInclude(d => d.Role)
                .Where(h => h.IsDeleted == false).Any(expression);
        }

        public async Task<Attendee> Get(Expression<Func<Attendee, bool>> expression)
        {
            return await _context.Attendees.Include(h => h.User)
                        .ThenInclude(b => b.UserRoles).ThenInclude(r => r.Role).Include(f => f.AttendeeCategories)
                        .ThenInclude( b => b.Category)
                        .Where(v => v.IsDeleted == false).FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Attendee>> GetAll()
        {
            return await _context.Attendees.Include(m => m.User)
                        .ThenInclude(v => v.UserRoles).ThenInclude(b => b.Role)
                        .Include(g => g.AttendeeCategories).ThenInclude (d => d.Category)
                        .Where(h => h.IsDeleted == false).ToListAsync();
        }
        
        public async Task<IList<Attendee>> GetAll(Expression<Func<Events, bool>> expression)
        {
            return await _context.Attendees.Include(h => h.User).ThenInclude(g => g.UserRoles).ThenInclude(l => l.Role)
                .Include(f => f.AttendeeCategories).ThenInclude(p => p.Category)
                .Where(f => f.IsDeleted == false).ToListAsync();
        }

        public Task<IList<Attendee>> GetAll(Expression<Func<Attendee, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Attendee>> GetSelected(Expression<Func<Attendee, bool>> expression)
        {
            return await _context.Attendees.Include(f => f.User).ThenInclude(l => l.UserRoles)
                        .ThenInclude(k => k.Role)
                        .Where(d => d.IsDeleted == false)
                        .Where(expression).ToListAsync();
        }

        public async Task<IList<Attendee>> GetSelected(IList<int> ids)
        {
            return await _context.Attendees.Include(h => h.User).ThenInclude(s => s.UserRoles)
                .ThenInclude(k => k.Role)
                .Where(d => d.IsDeleted == false).Where(h => ids.Contains(h.Id)).ToListAsync();
        }

        public async Task<IList<int>> GetAttendeeCategories(int attendeeId)
        {
            return await _context.AttendeeCategories.Include(p => p.Category).Include(j => j.Attendee)
                .Where(k => k.IsDeleted == false && k.AttendeeId == attendeeId ).Select(f => f.Category.Id).ToListAsync();
        }

        public bool EmailExist(string email)
        {
            return _context.Attendees.Any( Attendee => Attendee.Email.Equals(email));
        }

        public async Task<IList<Attendee>> GetAttendeesByEvent(int eventId)
        {
            return await _context.EventAttendees.Include(eventAttendee => eventAttendee.Attendee)
                .Where(f => f.IsDeleted == false && f.EventId == eventId)
                .Select(eventAttendee => eventAttendee.Attendee).ToListAsync();
        }

    }
}
