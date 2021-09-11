using BaseProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.DataAccess.EntityFramework
{
    public class EfQueryableRepositoryBase<TEntity, TContext> 
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private TContext context;
        private DbSet<TEntity> entities;
        public EfQueryableRepositoryBase(TContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Table { get { return this.entities; } }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (this.entities != null)
                    return this.entities;

                this.entities=this.context.Set<TEntity>();

                return this.entities;
            }
        }
    }
}
