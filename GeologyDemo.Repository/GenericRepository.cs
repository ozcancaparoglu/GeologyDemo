using GeologyDemo.Domain;
using GeologyDemo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeologyDemo.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : DomainBase
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> Table()
        {
            return context.Set<T>().AsQueryable();
        }

        public virtual async Task<ICollection<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Find(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual async Task<T> FindByProperties(Expression<Func<T, bool>> match, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();

            if (match != null)
                query = query.Where(match);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> Filter(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().Where(match).ToListAsync();
        }

        public virtual async Task<ICollection<T>> FilterWithProperties(Expression<Func<T, bool>> filter = null,
           string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
           int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return await query.ToListAsync();
        }

        public virtual async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public virtual T Update(T entity)
        {
            var entityEntry = context.Set<T>().Update(entity);
            return entityEntry.Entity;
        }

        public virtual async Task<int> Count()
        {
            return await context.Set<T>().CountAsync();
        }

        public virtual async Task<int> CountExpression(Expression<Func<T, bool>> predicate, bool isActive = true)
        {
            return await context.Set<T>().Where(predicate).CountAsync();
        }

        public virtual async Task BulkUpdate(ICollection<T> entities)
        {
            await context.BulkUpdateAsync(entities);
        }

        public virtual async Task BulkInsert(ICollection<T> entities)
        {
            await context.BulkInsertAsync(entities);
        }

        public virtual async Task BulkDelete(ICollection<T> entities)
        {
            await context.BulkDeleteAsync(entities);
        }
    }
}
