using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Abstractions
{
    public interface IRepository<T> where T : class,IEntityBase,new()
    { //soyut ladık nesneyi 
        Task AddAsync(T entity);

        Task<List<T>> GetAllAsycn(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties); //işlem yaparken bir tane değer dönmesini istediğimizde kullanacaz
        
        Task<T> GetByGuidAsync(Guid id);//id karşılık gelen entity bulabilecez

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T,bool>>predicate);

        Task<int> CountAsync(Expression<Func<T,bool>>predicate=null); 




    }
}
