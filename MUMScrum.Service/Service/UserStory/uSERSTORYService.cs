using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUMScrum.Model.Model;
using MUMScrum.Infrastructure.Core;
using MUMScrum.Repository;
using MUMScrum.Service.Service;
using MUMScrum.Model.ViewModel;
using System.Data.Entity;
using MUMScrum.Model.ENUMS;

namespace MUMScrum.Service
{
    public class UserstoryService : IUserstoryService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<UserStory> _UserstoryRepository;
        private readonly IRepository<WorkLog> _WorklogRepository;
        public UserstoryService()
        {

        }
        public UserstoryService(IUnitOfWork UnitOfWork, IRepository<UserStory> UserstoryRepository, IRepository<WorkLog> WorklogRepository)
        {
            _UnitOfWork = UnitOfWork;
            _UserstoryRepository = UserstoryRepository;
            _WorklogRepository = WorklogRepository;
        }
        public void Create(UserStory userstory)
        {
            if (userstory == null) throw new ArgumentNullException("userstory");
            this._UserstoryRepository.Add(userstory);
            this._UnitOfWork.Commit();
        }

        public void Delete(UserStory userstory)
        {
            if (userstory == null) throw new ArgumentNullException("Userstory");
            this._UserstoryRepository.Delete(userstory);
            this._UnitOfWork.Commit();
        }

        public IQueryable<UserStory> GetAll()
        {
            var test = this._UserstoryRepository.GetAll();
            return this._UserstoryRepository.GetAll().OrderBy(x => x.Id).AsQueryable();
        }

        public UserStory GetById(int Id)
        {
            return this._UserstoryRepository.GetById(Id);
        }

        public IQueryable<UserStory> GetUserstoryByDevId(int Id)
        {
            return this._UserstoryRepository.GetAll().Where(m => m.Developer.EmployeeId == Id).AsQueryable();
        }

        public IQueryable<UserStory> GetUserstoryByProjectId(int Id)
        {
            return this._UserstoryRepository.GetAll().Where(m => m.project.Id == Id).AsQueryable();
        }

        public IQueryable<UserStory> GetUserstoryByTesterId(int Id)
        {
            return this._UserstoryRepository.GetAll().Where(m => m.Tester.EmployeeId == Id).AsQueryable();
        }

        public EmployeeEffortEstimation GetEffortData(int empId, int userStoryId, RoleEnum role, EmployeeEstimateENUM effortType)
        {
            EmployeeEffortEstimation eee = new EmployeeEffortEstimation();
            UserStory us = this._UserstoryRepository.GetAll().Include(m => m.Developer).Where(m => m.Id == userStoryId).SingleOrDefault();
            eee.employee = (role == RoleEnum.Developer) ? us.Developer : (role == RoleEnum.Tester ? us.Tester : null);
            eee.userStory = us;
            eee.EmployeeId = empId;
            eee.UserStoryId = us.Id;
            if(effortType == EmployeeEstimateENUM.DevEstimate)
            eee.Effort = (role == RoleEnum.Developer) ? us.DeveloperEstimate : (role == RoleEnum.Tester ? us.TesterEstimate : 0);
            return eee;
        }

        public void UpdateEffortandEstimation(EmployeeEffortEstimation emp)
        {
            switch (emp.EstimateType)
            {
                case EmployeeEstimateENUM.DevEffort:
                case EmployeeEstimateENUM.TesterEffort:
                    WorkLog wl = new WorkLog();
                    wl.UserId = emp.EmployeeId;
                    wl.WorkCompleted = emp.Effort;
                    wl.UserStoryId = emp.UserStoryId;
                    this._WorklogRepository.Add(wl);
                    this._UnitOfWork.Commit();
                    break;

                case EmployeeEstimateENUM.DevEstimate:
                    UserStory us = this._UserstoryRepository.GetAll().Include(m => m.Developer).Where(m => m.Id == emp.UserStoryId).SingleOrDefault();
                    us.DeveloperEstimate = emp.Effort;
                    this._UserstoryRepository.Update(us);
                    this._UnitOfWork.Commit();
                    break;
                case EmployeeEstimateENUM.TesterEstimate:
                    UserStory us1 = this._UserstoryRepository.GetAll().Include(m => m.Tester).Where(m => m.Id == emp.UserStoryId).SingleOrDefault();
                    us1.TesterEstimate = emp.Effort;
                    this._UserstoryRepository.Update(us1);
                    this._UnitOfWork.Commit();
                    break;
            }
        }

        public void Update(UserStory userstory)
        {
            this._UserstoryRepository.Update(userstory);
            this._UnitOfWork.Commit();
        }
    }
}
