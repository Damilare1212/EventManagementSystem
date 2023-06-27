using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
  
        public Task<Comment> Get(Expression<Func<Comment, bool>> expression);
        public bool Exist(int id);
        public bool Exist(Expression<Func<Comment, bool>> expression);
        public Task<IList<Comment>> GetAll();
        public Task<IList<Comment>> GetSelected(IList<int> ids);
        public Task<IList<Comment>> GetSelected(Expression<Func<Comment, bool>> expression);

    }
}
