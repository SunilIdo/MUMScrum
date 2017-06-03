using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Service.Service
{
    public interface IGenericService<T>
    {
        T GetById(int Id);
        IQueryable<T> GetAll();

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
