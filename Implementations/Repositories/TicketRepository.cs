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
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationContext context)
        {
            _context = context;
        }
        public bool Exist(int id)
        {
            return _context.Tickets.Include(n => n.Event).Where(y => y.IsDeleted == false).Any(h => h.Id == id);
        }

        public bool Exist(Expression<Func<Ticket, bool>> expression)
        {
            return  _context.Tickets.Include(j => j.Event).Where(d => d.IsDeleted == false).Any(expression);
        }

        public async Task<Ticket> Get(Expression<Func<Ticket, bool>> expression)
        {
            return await _context.Tickets.Include(d => d.Event).
                Where(t => t.IsDeleted == false).FirstOrDefaultAsync(expression);

        }

        public async Task<IList<Ticket>> GetAll()
        {
            return await _context.Tickets.Include(h => h.Event).Where(l => l.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Ticket>> GetSelected(Expression<Func<Ticket, bool>> expression)
        {
            return await _context.Tickets.Include(k => k.Event).Where(c => c.IsDeleted == false)
                .Where(expression).ToListAsync();
        }

        public async Task<IList<Ticket>> GetSelected(IList<int> ids)
        {
            return await _context.Tickets.Include(m => m.Event).Where(g => g.IsDeleted == false)
                .Where(s => ids.Contains(s.Id)).ToListAsync();
        }
    }
}
