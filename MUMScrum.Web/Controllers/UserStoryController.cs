using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MUMScrum.Data;
using MUMScrum.Model.Model;
using MUMScrum.Service;
using MUMScrum.Model.ViewModel;
using MUMScrum.Model.ENUMS;
using MUMScrum.Model.SessionModel;

namespace MUMScrum.Web.Controllers
{
    public class UserStoryController : Controller
    {
        private IUserstoryService _userstoryService;
        private IEmployeeService _employeeService;
        private ISprintService _sprintService;
        private IReleaseBacklogService _releaseBacklogService;
        private IProjectService _projectService;

        public UserStoryController(IUserstoryService userstoryService,
            IEmployeeService employeeService,
            ISprintService sprintService,
            IReleaseBacklogService releaseBacklogService,
            IProjectService projectService)
        {
            this._userstoryService = userstoryService;
            this._employeeService = employeeService;
            this._sprintService = sprintService;
            this._releaseBacklogService = releaseBacklogService;
            this._projectService = projectService;
        }
        // GET: UserStory
        public ActionResult Index()
        {
            var userStorys = _userstoryService.GetAll().Include(u => u.Developer).Include(u => u.project).Include(u => u.ReleaseBacklog).Include(u => u.Sprint).Include(u => u.Tester);
            if (SessionModel.LoginUser.RoleId == RoleEnum.Developer)
            {
                userStorys = _userstoryService.GetAll().Include(u => u.Developer).Include(u => u.project).Include(u => u.ReleaseBacklog).Include(u => u.Sprint).Include(u => u.Tester).Where(m => m.DeveloperId == SessionModel.LoginUser.EmployeeId);
            }
            if (SessionModel.LoginUser.RoleId == RoleEnum.Tester)
            {
                userStorys = _userstoryService.GetAll().Include(u => u.Developer).Include(u => u.project).Include(u => u.ReleaseBacklog).Include(u => u.Sprint).Include(u => u.Tester).Where(m => m.TesterId == SessionModel.LoginUser.EmployeeId);
            }
            return View(userStorys.ToList());
        }

        // GET: UserStory/Details/5
        public ActionResult Details(int id)
        {
            UserStory userStory = _userstoryService.GetById(id);
            if (userStory == null)
            {
                return HttpNotFound();
            }
            return View(userStory);
        }

        // GET: UserStory/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name");
            //    ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name");
            //   ViewBag.DeveloperId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name");
            //  ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name");
            ViewBag.ReleaseBacklogId = new SelectList(_releaseBacklogService.GetAll(), "Id", "Name");
            //  ViewBag.SprintId = new SelectList(_sprintService.GetAll(), "Id", "Name");
            //  ViewBag.TesterId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name");
            return View();
        }

        // POST: UserStory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,UserStoryType,Status,ProjectId,DeveloperId,TesterId,DeveloperEstimate,TesterEstimate,ReleaseBacklogId,SprintId,CreatedById,ModifiedById,CreatedDate,ModifiedDate")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                _userstoryService.Create(userStory);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name", userStory.ProjectId);
            ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.CreatedById);
            ViewBag.DeveloperId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.DeveloperId);
            ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.ModifiedById);
            ViewBag.ReleaseBacklogId = new SelectList(_releaseBacklogService.GetAll(), "Id", "Name", userStory.ReleaseBacklogId);
            ViewBag.SprintId = new SelectList(_sprintService.GetAll(), "Id", "Name", userStory.SprintId);
            ViewBag.TesterId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.TesterId);
            return View(userStory);
        }

        // GET: UserStory/Edit/5
        public ActionResult Edit(int id)
        {
            UserStory userStory = _userstoryService.GetById(id);
            if (userStory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name", userStory.ProjectId);
            //ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.CreatedById);
            //ViewBag.DeveloperId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.DeveloperId);
            //ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.ModifiedById);
            ViewBag.ReleaseBacklogId = new SelectList(_releaseBacklogService.GetAll(), "Id", "Name", userStory.ReleaseBacklogId);
            //ViewBag.SprintId = new SelectList(_sprintService.GetAll(), "Id", "Name", userStory.SprintId);
            //ViewBag.TesterId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.TesterId);
            return View(userStory);
        }

        // POST: UserStory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,UserStoryType,Status,ProjectId,DeveloperId,TesterId,DeveloperEstimate,TesterEstimate,ReleaseBacklogId,SprintId,CreatedById,ModifiedById,CreatedDate,ModifiedDate")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                _userstoryService.Update(userStory);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name", userStory.ProjectId);
            // ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.CreatedById);
            // ViewBag.DeveloperId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.DeveloperId);
            // ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.ModifiedById);
            ViewBag.ReleaseBacklogId = new SelectList(_releaseBacklogService.GetAll(), "Id", "Name", userStory.ReleaseBacklogId);
            //  ViewBag.SprintId = new SelectList(_sprintService.GetAll(), "Id", "Name", userStory.SprintId);
            //   ViewBag.TesterId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", userStory.TesterId);
            return View(userStory);
        }

        // GET: UserStory/Delete/5
        public ActionResult Delete(int id)
        {
            UserStory userStory = _userstoryService.GetById(id);
            if (userStory == null)
            {
                return HttpNotFound();
            }
            return View(userStory);
        }

        // POST: UserStory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserStory userStory = _userstoryService.GetById(id);
            _userstoryService.Delete(userStory);
            return RedirectToAction("Index");
        }

        public ActionResult DeveloperEstimateEffort(int empId, int userStoryId, EmployeeEstimateENUM effortType)
        {
            var Role = SessionModel.LoginUser.RoleId;
            EmployeeEffortEstimation emp = this._userstoryService.GetEffortData(empId, userStoryId, Role, effortType);
            emp.EstimateType = effortType;
            return View(emp);
        }
        [HttpPost]
        public ActionResult DeveloperEstimateEffort(EmployeeEffortEstimation emp)
        {
            this._userstoryService.UpdateEffortandEstimation(emp);
            //this._userstoryService.
            //EmployeeEffortEstimation emp = this._userstoryService.GetEffortData(empId, userStoryId);
            return RedirectToAction("Index");
        }


    }
}
