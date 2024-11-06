using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public class EfRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
       where TContext : DbContext, new()
    {

        IHttpContextAccessor _httpContext;
        public EfRepositoryBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                using var context = new TContext();
                entity.AddUserId = _httpContext.HttpContext.User.Identity.Name;
                entity.AddDate = DateTime.Now;
                entity.EditUserId = _httpContext.HttpContext.User.Identity.Name;
                entity.EditDate = DateTime.Now;
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using var context = new TContext();
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public IQueryable<TEntity> FindByAsync(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().Where(filter).AsNoTracking();
            return query;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null)
        {
            using var context = new TContext();
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return filter == null ? await query.SingleOrDefaultAsync() : await query.SingleOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null)
        {
            try
            {
                using var context = new TContext();
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return filter == null ? await query.OrderBy(c => c.AddDate).ToListAsync() : await query.Where(filter).OrderBy(c => c.AddDate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var context = new TContext();
            entity.AddUserId = entity.AddUserId;
            entity.AddDate = DateTime.Now;
            entity.EditUserId = _httpContext.HttpContext.User.Identity.Name;
            entity.EditDate = DateTime.Now;
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return updatedEntity.Entity;
        }
    }
}
