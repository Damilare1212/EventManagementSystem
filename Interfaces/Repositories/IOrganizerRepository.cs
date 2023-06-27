using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface IOrganizerRepository : IRepository<Organizer>
    {
        public Task<Organizer> Get(Expression<Func<Organizer, bool>> expression);
        public Task<IList<Organizer>> GetSelected(Expression<Func<Organizer, bool>> expression);
        public Task<IList<Organizer>> GetSelected(IList<int> ids);
        public Task<IList<Organizer>> GetAll();
        public bool Exist(Expression<Func<Organizer, bool>> expression);
        public bool Exist(int id);
    }
}
