using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<Role> GetByName(string name);
        public Task<IList<Role>> GetSelected(Expression<Func<Role, bool>> expression);
        public Task<IList<Role>> GetSelected(IList<int> ids);
        public Task<IList<Role>> GetAll();
        public bool Exist(int id);
        public bool Exist(Expression<Func<Role, bool>> expression);
    }
}
