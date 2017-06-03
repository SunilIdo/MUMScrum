using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Service
{
    public interface IGenericService<T>
    {
        T GetById(int Id);
        IQueryable<T> GetAll();

        void Create(T Entity);

        void Update(T Entity);

        void Delete(T Entity);

    }
}
