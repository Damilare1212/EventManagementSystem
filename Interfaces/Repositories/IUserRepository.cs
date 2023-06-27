using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> Get(Expression<Func<User, bool>> expression);
        public Task<IList<User>> GetSelected(IList<int> ids);
        public Task<IList<User>> GetSelected(Expression<Func<User, bool>> expression);      
        public bool Exists(Expression<Func<User, bool>> expression);
        public bool Exist(int id);
        public Task<User> GetUserByEmail(string email);
    }
}
