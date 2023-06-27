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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public bool Exists(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Include(m => m.UserRoles).ThenInclude(h => h.Role)
                .Where(t => t.IsDeleted == false).Any(expression);
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.Include(U => U.UserRoles).ThenInclude(m => m.Role)
                .Where(v => v.IsDeleted == false).FirstOrDefaultAsync(expression);
          
        }
        public async Task<IList<User>> GetSelected(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.Include(u => u.UserRoles).ThenInclude(g => g.Role)
                         .Where(s => s.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<User>> GetAll()
        {
            return await _context.Users.Include(s => s.UserRoles).ThenInclude(j => j.Role)
                .Where(h => h.IsDeleted == false).ToListAsync();

        }

        public async Task<IList<User>> GetSelected(IList<int> ids)
        {
            return await _context.Users.Include(d => d.UserRoles).ThenInclude(h => h.Role)
               .Where(k => k.IsDeleted == false).Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public bool Exist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(h => h.UserRoles).ThenInclude(k => k.Role)
                .Where(m => m.IsDeleted == false).FirstOrDefaultAsync( j => j.Email == email);
        }
    }
}
