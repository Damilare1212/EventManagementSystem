using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface IAttendeeRepository : IRepository<Attendee>
    {
        
        public Task<Attendee> Get(Expression<Func<Attendee, bool>> expression);
        public Task<IList<Attendee>> GetSelected(Expression<Func<Attendee, bool>> expression);
        public Task<IList<Attendee>> GetSelected(IList<int> ids);
        public Task<IList<Attendee>> GetAll();
        public Task<IList<int>> GetAttendeeCategories(int attendeeId);
        public Task<IList<Attendee>> GetAll(Expression<Func<Attendee, bool>> expression);
        public bool Exist(int id);
        public bool Exists(Expression<Func<Attendee, bool>> expression);
        public bool EmailExist(string email);
        public Task<IList<Attendee>> GetAttendeesByEvent(int eventId);
    }
}
