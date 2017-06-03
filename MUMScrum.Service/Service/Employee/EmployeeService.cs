using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUMScrum.Model.Model;
using MUMScrum.Infrastructure.Core;
using MUMScrum.Repository;

namespace MUMScrum.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<Employee> _EmployeeRepository;
        public EmployeeService()
        {

        }
        public EmployeeService(IUnitOfWork UnitOfWork, IRepository<Employee> EmployeeRepository)
        {
            _UnitOfWork = UnitOfWork;
            _EmployeeRepository = EmployeeRepository;
        }  
        public void Create(Employee Employee)
        {
            if (Employee == null) throw new ArgumentNullException("employee");
            this._EmployeeRepository.Add(Employee);
            this._UnitOfWork.Commit();
        }

        public void Delete(Employee Employee)
        {
            if (Employee == null) throw new ArgumentNullException("employee");
            this._EmployeeRepository.Delete(Employee);
            this._UnitOfWork.Commit();
        }

        public IQueryable<Employee> GetAll()
        {
            return this._EmployeeRepository.GetAll().OrderBy(x => x.EmployeeId).AsQueryable();
        }

        public Employee GetById(int Id)
        {
            return this._EmployeeRepository.GetById(Id);
        }

        public void Update(Employee Employee)
        {        
            this._EmployeeRepository.Update(Employee);
            this._UnitOfWork.Commit();
        }
    }
}
