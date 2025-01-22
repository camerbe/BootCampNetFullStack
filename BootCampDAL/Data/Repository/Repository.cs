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
        }

        public async Task<IEnumerable<T>> AddRange(T entity)
        {
             await _db.AddRangeAsync(entity);    
        }

        public Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }
    }
}
