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
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Exist(int id)
        {
            return _context.Admins.Include(j => j.User).ThenInclude(h => h.UserRoles)
                .ThenInclude(l => l.Role).Where(x => x.IsDeleted == false).Any(n => n.Id == id);
        }

        public bool Exists(Expression<Func<Admin, bool>> expression)
        {
            return _context.Admins.Include(t => t.User)
                    .ThenInclude(m => m.UserRoles).ThenInclude(n => n.Role)
                    .Where(z => z.IsDeleted == false).Any(expression);
        }

        public async Task<Admin> Get(Expression<Func<Admin, bool>> expression)
        {
            var admin = await _context.Admins.Include(a => a.User)
                        .ThenInclude(h => h.UserRoles)
                        .ThenInclude(f => f.Role)
                        .Where(d => d.IsDeleted == false)
                        .FirstOrDefaultAsync(expression);
            return admin;
        }

        public async Task<IList<Admin>> GetAll()
        {
            var res = await _context.Admins.Include(b => b.User).ThenInclude(l => l.UserRoles)
                        .ThenInclude(m => m.Role)
                         .Where(r => r.IsDeleted == false).ToListAsync();
            return res;
                       
        }

        
        public async Task<IList<Admin>> GetSelected(Expression<Func<Admin, bool>> expression)
        {
            return await _context.Admins.Include(h => h.User)
                        .ThenInclude(s => s.UserRoles).ThenInclude(v => v.Role)
                        .Where(b => b.IsDeleted == false)
                        .Where(expression).ToListAsync();
        }

        public async Task<IList<Admin>> Getselected(IList<int> ids)
        {
            return await _context.Admins.Include(g => g.User).ThenInclude(h => h.UserRoles)
                .ThenInclude(k => k.Role).Where(k => k.IsDeleted == false).ToListAsync();
        }
    }
}
