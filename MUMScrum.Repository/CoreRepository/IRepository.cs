using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Infrastructure.Core
{

    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllWithNavigation(string[] children);
        IQueryable<T> GetAllWithNavigation(string[] children, Expression<Func<T, bool>> where);
        T GetById(int Id);
        void Add(T entity);
        void Update(T entity);
        void Save(T entity);
        void Delete(T entity);
        void Delete(int Id);
        Task DeleteAsync(int Id);
        Task DeleteAsync(Expression<Func<T, bool>> match);
        IEnumerable<T> GetFiltered(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllQueryable();
        IQueryable<T> Table { get; }
        IQueryable<T> TableAsNoTracking { get; }

    }
}
