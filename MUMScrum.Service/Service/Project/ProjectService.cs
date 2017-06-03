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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<Project> _ProjectRepository;
        public ProjectService()
        {

        }
        public ProjectService(IUnitOfWork UnitOfWork, IRepository<Project> ProjectRepository)
        {
            _UnitOfWork = UnitOfWork;
            _ProjectRepository = ProjectRepository;
        }  
        public void Create(Project project)
        {
            if (project == null) throw new ArgumentNullException("Project");
            this._ProjectRepository.Add(project);
            this._UnitOfWork.Commit();
        }

        public void Delete(Project project)
        {
            if (project == null) throw new ArgumentNullException("Project");
            this._ProjectRepository.Delete(project);
            this._UnitOfWork.Commit();
        }

        public IQueryable<Project> GetAll()
        {
            return this._ProjectRepository.GetAll().OrderBy(x => x.Id).AsQueryable();
        }

        public Project GetById(int Id)
        {
            return this._ProjectRepository.GetById(Id);
        }

        public void Update(Project project)
        {        
            this._ProjectRepository.Update(project);
            this._UnitOfWork.Commit();
        }
    }
}
