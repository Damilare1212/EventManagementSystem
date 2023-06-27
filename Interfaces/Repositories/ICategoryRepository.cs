using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<Category> Get(Expression<Func<Category, bool>> expression);
        public Task<IList<Category>> GetSelected(Expression<Func<Category, bool>> expression);
        public Task<IList<Category>> GetSelected(IList<int> ids);
        public bool Exists(Expression<Func<Category, bool>> expression);
        public bool Exist(int id);
        public Task<IList<Category>> GetAll();
        public Task<IList<Category>> GetCategoriesByEvent(int eventId);
       
    }
}
