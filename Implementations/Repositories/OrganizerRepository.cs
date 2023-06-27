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
    public class OrganizerRepository : BaseRepository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(ApplicationContext context)
        {
            _context = context;  
        }

        public bool Exist(Expression<Func<Organizer, bool>> expression)
        {
            return _context.Organizers.Include(h => h.User)
                .ThenInclude(m => m.UserRoles).ThenInclude(b => b.Role)
                .Include(k => k.EventOrganizers).ThenInclude(s => s.Event)
                .Where(k => k.IsDeleted == false).Any(expression);
        }

        public bool Exist(int id)
        {
            return _context.Organizers.Include(b => b.EventOrganizers)
                .ThenInclude(m => m.Event).Include(l => l.User)
                .ThenInclude(d => d.UserRoles).ThenInclude(k  => k.Role)
                .Where(v => v.IsDeleted == false).Any(m => m.Id == id);
        }

        
        public async Task<Organizer> Get(Expression<Func<Organizer, bool>> expression)
        {
            return await _context.Organizers.Include(d => d.User)
                        .ThenInclude(p => p.UserRoles).ThenInclude(m => m.Role)
                        .Include(l => l.EventOrganizers).ThenInclude(m => m.Event)
                        .Where(g => g.IsDeleted == false).FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Organizer>> GetAll()
        {
            return await _context.Organizers.Include(p => p.User)
                    .ThenInclude(k => k.UserRoles).ThenInclude(n => n.Role)
                    .Include(b => b.EventOrganizers).ThenInclude(h => h.Event)
                    .Where(t => t.IsDeleted == false).ToListAsync();
        }
        public async Task<IList<Organizer>> GetSelected(Expression<Func<Organizer, bool>> expression)
        {
            return await _context.Organizers.Include(s => s.User)
                        .ThenInclude(v => v.UserRoles).ThenInclude(d => d.Role)
                        .Include(h => h.EventOrganizers).ThenInclude(f => f.Event)
                        .Where(j => j.IsDeleted == false)
                        .Where(expression).ToListAsync();
        }

        public async Task<IList<Organizer>> GetSelected(IList<int> ids)
        {
            return await _context.Organizers.Include(h => h.User)
                .ThenInclude(g => g.UserRoles).ThenInclude(n => n.Role)
                .Include(h => h.EventOrganizers).ThenInclude(d => d.Event)
                .Where(h => h.IsDeleted == false).Where(k => ids.Contains(k.Id)).ToListAsync();
        }
    }
}
