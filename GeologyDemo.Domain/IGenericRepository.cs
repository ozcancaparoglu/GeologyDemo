using GeologyDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeologyDemo.Domain
{
    public interface IGenericRepository<T> where T : DomainBase
    {
        Task Add(T entity);
        Task BulkDelete(ICollection<T> entities);
        Task BulkInsert(ICollection<T> entities);
        Task BulkUpdate(ICollection<T> entities);
        Task<int> Count();
        Task<int> CountExpression(Expression<Func<T, bool>> predicate, bool isActive = true);
        Task<ICollection<T>> Filter(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FilterWithProperties(Expression<Func<T, bool>> filter = null,
           string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           int? page = null, int? pageSize = null);
        Task<T> Find(Expression<Func<T, bool>> match);
        Task<T> FindByProperties(Expression<Func<T, bool>> match, string includeProperties = "");
        Task<ICollection<T>> GetAll();
        Task<T> GetById(int id);
        IQueryable<T> Table();
        T Update(T entity);
    }
}
