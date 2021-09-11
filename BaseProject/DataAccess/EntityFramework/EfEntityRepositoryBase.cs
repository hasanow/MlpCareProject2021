using BaseProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
        where TContext:DbContext,new()
    {
        protected TContext context; 

        public EfEntityRepositoryBase(TContext context)
        {
           this.context = context;
        }

        public TEntity Add(TEntity entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
            context.ChangeTracker.Clear();
            return addedEntity.Entity;
        }
        

        public void Delete(TEntity entity)
        {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();            
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
           
                return context.Set<TEntity>().AsNoTracking().SingleOrDefault(filter);
            
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
           
                return filter == null
                    ? context.Set<TEntity>().AsNoTracking().ToList()
                    : context.Set<TEntity>().AsNoTracking().Where(filter).ToList();
            
        }
        TEntity IEntityRepository<TEntity>.Update(TEntity entity)
        {
            context.ChangeTracker.Clear();
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Modified;
                context.SaveChanges();
            context.ChangeTracker.Clear();

            return addedEntity.Entity;            
        }
    }
}
