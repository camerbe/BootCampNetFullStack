using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        Task<IEnumerable<T>>GetAll(Expression<Func<T,bool>> expression=null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,
            List<string> includes=null
            );
        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes);
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRange(T entity);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entity);


    }
    
}
