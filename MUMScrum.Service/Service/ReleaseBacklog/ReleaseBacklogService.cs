using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUMScrum.Model.Model;
using MUMScrum.Infrastructure.Core;

namespace MUMScrum.Service
{
    public class ReleaseBacklogService : IReleaseBacklogService
    {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<ReleaseBacklog> _ReleaseBacklogRepository;

        public ReleaseBacklogService()
        {

        }
        public ReleaseBacklogService(IUnitOfWork UnitOfWork, IRepository<ReleaseBacklog> ReleaseBacklogRepository)
        {
            _UnitOfWork = UnitOfWork;
            _ReleaseBacklogRepository = ReleaseBacklogRepository;
        }
        public void Create(ReleaseBacklog entity)
        {
            if (entity == null) throw new ArgumentNullException("releasebacklog");
            this._ReleaseBacklogRepository.Add(entity);
            this._UnitOfWork.Commit();
        }

        public void Delete(ReleaseBacklog entity)
        {
            if (entity == null) throw new ArgumentNullException("ReleaseBacklog");
            this._ReleaseBacklogRepository.Delete(entity);
            this._UnitOfWork.Commit();
        }

        public IQueryable<ReleaseBacklog> GetAll()
        {
            var test = this._ReleaseBacklogRepository.GetAll();
            return this._ReleaseBacklogRepository.GetAll().OrderBy(x => x.Id).AsQueryable();
        }

        public Model.Model.ReleaseBacklog GetById(int Id)
        {
            return this._ReleaseBacklogRepository.GetById(Id);
        }

        public void Update(ReleaseBacklog entity)
        {
            this._ReleaseBacklogRepository.Update(entity);
            this._UnitOfWork.Commit();
        }
    }
}
