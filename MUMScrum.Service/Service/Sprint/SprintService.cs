using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUMScrum.Model.Model;
using MUMScrum.Infrastructure.Core;
using MUMScrum.Repository;
using MUMScrum.Service.Service;

namespace MUMScrum.Service
{
    public class SprintService : ISprintService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<Sprint> _SprintRepository;
        public SprintService()
        {

        }
        public SprintService(IUnitOfWork UnitOfWork, IRepository<Sprint> SprintRepository)
        {
            _UnitOfWork = UnitOfWork;
            _SprintRepository = SprintRepository;
        }  
        public void Create(Sprint sprint)
        {
            if (sprint == null) throw new ArgumentNullException("Sprint");
            this._SprintRepository.Add(sprint);
            this._UnitOfWork.Commit();
        }

        public void Delete(Sprint sprint)
        {
            if (sprint == null) throw new ArgumentNullException("Sprint");
            this._SprintRepository.Delete(sprint);
            this._UnitOfWork.Commit();
        }

        public IQueryable<Sprint> GetAll()
        {
            return this._SprintRepository.GetAll().OrderBy(x => x.Id).AsQueryable();
        }

        public Sprint GetById(int Id)
        {
            return this._SprintRepository.GetById(Id);
        }


        public void Update(Sprint sprint)
        {        
            this._SprintRepository.Update(sprint);
            this._UnitOfWork.Commit();
        }
    }
}
