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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }
        public bool Exist(Expression<Func<Role, bool>> expression)
        {
            return _context.Roles.Include(k => k.UserRoles).ThenInclude(r => r.User)
                    .Where(v => v.IsDeleted == false).Any(expression);
        }

        public bool Exist(int id)
        {
            return _context.Roles.Include(h => h.UserRoles)
                    .ThenInclude(j => j.User).Where(h => h.IsDeleted == false)
                    .Any(f => f.Id == id);
        }



        public async Task<Role> GetById(int id)
        {
            return await _context.Roles.Include(d => d.UserRoles).ThenInclude(f => f.User)
                        .Where(j => j.IsDeleted == false)
                        .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Role> GetByName(string name)
        {
            return await _context.Roles.Include(f => f.UserRoles).ThenInclude(g => g.User)
                .Where(d => d.IsDeleted == false).FirstOrDefaultAsync(j => j.Name.ToLower() == name.ToLower().Trim());
        }

        public async Task<IList<Role>> GetSelected(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles.Include(b => b.UserRoles)
                        .Where(c => c.IsDeleted == false)
                        .Where(expression).ToListAsync();
        }

        public async Task<IList<Role>> GetSelected(IList<int> ids)
        {
            return await _context.Roles.Include(h => h.UserRoles).ThenInclude(d => d.User)
                    .Where(h => h.IsDeleted == false)
                    .Where(k => ids.Contains(k.Id)).ToListAsync();
        }

        public async Task<IList<Role>> GetAll()
        {
            return  await _context.Roles.Include(m => m.UserRoles).ThenInclude(b => b.User)
                        .Where(p => p.IsDeleted == false).ToListAsync();
        }
    }
}
