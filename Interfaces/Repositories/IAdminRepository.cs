using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface IAdminRepository : IRepository<Admin>
    {
        public Task<Admin> Get(Expression<Func<Admin, bool>> expression);
        public Task<IList<Admin>> GetAll();
        public Task<IList<Admin>> Getselected(IList<int> ids);
        public Task<IList<Admin>> GetSelected(Expression<Func<Admin, bool>> expression);
        public bool Exist(int id);
        public bool Exists(Expression<Func<Admin, bool>> expression);
        
    }
}
