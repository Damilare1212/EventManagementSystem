using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Get(int id);
        void SaveChanges();   
    }
}
