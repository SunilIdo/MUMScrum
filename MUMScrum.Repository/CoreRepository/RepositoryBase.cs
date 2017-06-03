using MUMScrum.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Infrastructure.Core
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _dataContext;
        private DbSet<T> _dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }
        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }
        protected ApplicationDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }
        public IQueryable<T> Table
        {
            get
            {
                return _dbset;
            }
        }

        public IQueryable<T> TableAsNoTracking
        {
            get
            {
                return _dbset.AsNoTracking();
            }
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(int Id)
        {
            var entity = _dbset.Find(Id);
            _dbset.Remove(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> match)
        {
            var entities = await _dbset.Where(match).ToListAsync();
            foreach(var entity in entities)
            {
                _dbset.Remove(entity);
            }
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await _dbset.FindAsync(Id);
            _dbset.Remove(entity);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).AsQueryable<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbset.AsQueryable<T>();
        }

        public virtual IQueryable<T> GetAllQueryable()
        {
            return _dbset.AsQueryable<T>();
        }

        public IQueryable<T> GetAllWithNavigation(string[] children)
        {
            if (children != null)
            {
                foreach(var child in children)
                {
                    _dbset.Include(child);
                }
            }
            return _dbset;
        }

        public IQueryable<T> GetAllWithNavigation(string[] children, Expression<Func<T, bool>> where)
        {
            if (children != null)
            {
                foreach(var child in children)
                {
                    _dbset.Include(child);
                }
            }
            return _dbset.Where(where);
        }

        public T GetById(int Id)
        {
            return _dbset.Find(Id);
        }

        public virtual IEnumerable<T> GetFiltered(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public virtual async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> where)
        {
            return await Task.FromResult<IEnumerable<T>>(_dbset.Where(where).ToList());
        }

        public void Save(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
