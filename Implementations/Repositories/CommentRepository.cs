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
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Exist(int id)
        {
            return _context.Comments.Where(f => f.IsDeleted == false).Any(m => m.Id == id);
        }

        public bool Exist(Expression<Func<Comment, bool>> expression)
        {
            return _context.Comments.Where(h => h.IsDeleted == false).Any(expression);
        }

        
        public async Task<Comment> Get(Expression<Func<Comment, bool>> expression)
        {
            return await _context.Comments.Where(d => d.IsDeleted == false)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Comment>> GetAll()
        {
            return await _context.Comments.Where(k => k.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Comment>> GetSelected(Expression<Func<Comment, bool>> expression)
        {
            return await _context.Comments.Where(q => q.IsDeleted == false)
                .Where(expression).ToListAsync();
        }

        public async Task<IList<Comment>> GetSelected(IList<int> ids)
        {
            return await _context.Comments.Where(k => k.IsDeleted == false)
                .Where(a => ids.Contains(a.Id)).ToListAsync();
        }
    }
}
