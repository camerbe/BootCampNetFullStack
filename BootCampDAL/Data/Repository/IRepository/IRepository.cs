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
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRange(T entity);
        Task Remove(Guid id);
        void RemoveRange(IEnumerable<T> entity);


    }
    
}
