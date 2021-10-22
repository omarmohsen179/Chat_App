using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chat_App.Core
{
    public class SkipAndTake

    {
        public int TotalCount { get; set; }
        public int CurrentCount { get; set; }
        public IEnumerable<Object> Data { get; set; }
    }
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {

        private readonly TContext context;
        private DbSet<TEntity> set;

        public Repository(TContext context)
        {
            this.context = context;
            set = context.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity entity)
        {
            set.Add(entity);
            return context.SaveChanges() > 0 ? entity : null;
        }
        public virtual IEnumerable<TEntity> AddList(IEnumerable<TEntity> entity)
        {
            set.AddRange(entity);
            return context.SaveChanges() > 0 ? entity : null;
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            return set;

        }
        public virtual IQueryable<TEntity> GetAllReverse()
        {
            return set.Reverse();

        }

        public virtual SkipAndTake SkipAndTake(int skip, string includeProperties = "")
        {
            var Wanted = set.Count() - skip;
            if (Wanted < 0)
            {
                Wanted = 0;
            }
            IQueryable<TEntity> query = set;
            foreach (var includeProperty in includeProperties.Split
    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return new SkipAndTake
            {
                TotalCount = set.Count(),
                CurrentCount = set.OrderBy(g => g).Skip(Wanted).Count(),
                Data = query.OrderBy(g => g).Skip(Wanted).Reverse()
            };


        }
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = set;

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
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual List<TEntity> GetAllBind()
        {
            return set.ToList();
        }

        public virtual TEntity GetById(params object[] id)
        {
            return set.Find(id);
        }

        public virtual bool IsExists(params object[] id)
        {
            TEntity entity = set.Find(id);
            return entity != null ? true : false;
        }

        public virtual bool Remove(TEntity entity)
        {
            set.Remove(entity);
            return context.SaveChanges() > 0;
        }

        public virtual bool RemoveById(params object[] id)
        {
            TEntity result = set.Find(id);
            set.Remove(result);
            return context.SaveChanges() > 0;
        }

        public virtual bool Update(TEntity entity)
        {
            set.Update(entity);
            return context.SaveChanges() > 0;
        }
        public virtual bool UpdateList(IEnumerable<TEntity> entity)
        {
            set.UpdateRange(entity);
            return context.SaveChanges() > 0;
        }
    }
}
