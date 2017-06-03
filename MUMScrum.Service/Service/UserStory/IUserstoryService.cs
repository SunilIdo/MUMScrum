using MUMScrum.Model.ENUMS;
using MUMScrum.Model.Model;
using MUMScrum.Model.ViewModel;
using MUMScrum.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Service
{
    public interface IUserstoryService : IGenericService<UserStory>
    {
        IQueryable<UserStory> GetUserstoryByDevId(int Id);
        IQueryable<UserStory> GetUserstoryByTesterId(int Id);
        IQueryable<UserStory> GetUserstoryByProjectId(int Id);
        EmployeeEffortEstimation GetEffortData(int empId, int userStoryId, RoleEnum role, EmployeeEstimateENUM effortType);
        void UpdateEffortandEstimation(EmployeeEffortEstimation emp);
    }
}
