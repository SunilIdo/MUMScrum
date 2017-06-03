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
    public class WorklogService : IWorklogService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<WorkLog> _WorklogRepository;
        public WorklogService()
        {

        }
        public WorklogService(IUnitOfWork UnitOfWork, IRepository<WorkLog> WorklogRepository)
        {
            _UnitOfWork = UnitOfWork;
            _WorklogRepository = WorklogRepository;
        }  
        public void Create(WorkLog worklog)
        {
            if (worklog == null) throw new ArgumentNullException("Worklog");
            this._WorklogRepository.Add(worklog);
            this._UnitOfWork.Commit();
        }

        public void Delete(WorkLog worklog)
        {
            if (worklog == null) throw new ArgumentNullException("Worklog");
            this._WorklogRepository.Delete(worklog);
            this._UnitOfWork.Commit();
        }

        public IQueryable<WorkLog> GetAll()
        {
            return this._WorklogRepository.GetAll().OrderBy(x => x.Id).AsQueryable();
        }

        public WorkLog GetById(int Id)
        {
            return this._WorklogRepository.GetById(Id);
        }

        public void Update(WorkLog worklog)
        {        
            this._WorklogRepository.Update(worklog);
            this._UnitOfWork.Commit();
        }
    }
}
