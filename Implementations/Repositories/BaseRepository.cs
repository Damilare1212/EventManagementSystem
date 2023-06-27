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
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected ApplicationContext _context;

        public async Task <T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().Where(b => b.IsDeleted == false).FirstOrDefaultAsync(g => g.Id == id);

        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _context.Set<T>().Where(k => k.IsDeleted == false).ToListAsync();
        }

        public void SaveChanges()
        {
              _context.SaveChanges();
        }

        public async Task<T> Update(T entity)
        {
             _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


    }
}
