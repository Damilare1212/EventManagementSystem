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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Exist(int id)
        {
            return _context.Categories
                    .Where(j => j.IsDeleted == false).Any(k => k.Id == id);
        }

        public bool Exists(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.Where(n => n.IsDeleted == false).Any(expression);
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories.Where(j => j.IsDeleted == false).FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Category>> GetAll()
        {
            return await _context.Categories.Where(h => h.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Category>> GetCategoriesByEvent(int eventId)
        {
            return await _context.EventCategories.Include(c => c.Category).Where(a => a.EventId == eventId).Select(a => a.Category).ToListAsync();
        }

        public async Task<IList<Category>> GetSelected(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories.Where(k => k.IsDeleted == false).Where(expression).ToListAsync();
        }

        public async Task<IList<Category>> GetSelected(IList<int> ids)
        {
            var categories = await _context.Categories.Include( k => k.EventCategories).ThenInclude(h => h.Event)
               .Where(b => b.IsDeleted == false).Where(n => ids.Contains(n.Id)).ToListAsync();
            return categories;
        }
    }
}
