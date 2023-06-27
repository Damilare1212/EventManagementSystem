using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        public Task<Ticket> Get(Expression<Func<Ticket, bool>> expression);
        public Task<IList<Ticket>> GetSelected(Expression<Func<Ticket, bool>> expression);
        public bool Exist(Expression<Func<Ticket, bool>> expression);
        public bool Exist(int id);
        public Task<IList<Ticket>> GetAll();
        public Task<IList<Ticket>> GetSelected(IList<int> ids);
    }
}
