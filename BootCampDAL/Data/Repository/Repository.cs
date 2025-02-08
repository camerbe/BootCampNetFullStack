using BootCampDAL.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Repository
{
    public class Repository<T>   : IRepository<T> where T : class
    {
        private readonly BootCampDalContext _context;
        private readonly DbSet<T> _db;

        public Repository(BootCampDalContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
             await _db.AddAsync(entity);
             return entity;
        }

        public async Task<IEnumerable<T>> AddRange(T entity)
        {
             await _db.AddRangeAsync(entity);
             return (IEnumerable<T>)entity;
        }

        public async Task<T?> Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _db;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryable = queryable.Include(include);
                }
            }
            return await queryable.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IQueryable<T> queryable = _db;
            return queryable.ToList();
        }

        async Task IRepository<T>.Remove(Guid id)
        {
            var entity=await _db.FindAsync(id);
            _db.Remove(entity);
        }

        
        void IRepository<T>.RemoveRange(IEnumerable<T> entity)
        {
             _db.RemoveRange(entity);
        }
    }
}
