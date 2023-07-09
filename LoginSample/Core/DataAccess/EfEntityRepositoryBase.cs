using Core.DataAccess.Extensions;
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Create(TEntity entity)
        {
            using (var context = new TContext())
            {
                var entityToAdd = context.Entry(entity);
                entityToAdd.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var entityToDelete = context.Entry(entity);
                entityToDelete.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        // GetAll for check if role exist
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                if (!context.Set<TEntity>().Any())
                    return Enumerable.Empty<TEntity>();

                return filter == null
                    ? context.Set<TEntity>().AsNoTrackingWithIdentityResolution().ToList()
                    : context.Set<TEntity>().Where(filter).AsNoTrackingWithIdentityResolution().ToList();
            }
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, int page = 0, int size = 25)
        {
            using (var context = new TContext())
            {
                if (!context.Set<TEntity>().Any())
                    return Enumerable.Empty<TEntity>();

                return filter == null
                    ? context.Set<TEntity>().ToPaginate(page, size)
                    : context.Set<TEntity>().Where(filter).ToPaginate(page, size);
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var entityToUpdate = context.Entry(entity);
                entityToUpdate.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
