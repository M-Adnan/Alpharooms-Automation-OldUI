using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AlphaRoomsTests.DbContext;

namespace AlphaRoomsTests.Repositories
{
    public class BaseRepository<TEntity> where
        TEntity:class
    {
        internal IDbContext Context;
        internal IDbSet<TEntity> DbSet;

        public BaseRepository(IDbContext context)
        {
            Context = context;
            this.DbSet = Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int take = 0, int skip = 0)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }


            return query.ToList();

        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }
    }
}
