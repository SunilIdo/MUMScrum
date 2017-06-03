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

namespace MUMScrum.Web.Controllers
{
    public class ProjectController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IProjectService _projectService;
        private IUserstoryService _userStoryService;
        private IEmployeeService _employeeService;

        public ProjectController(IProjectService projectService, IUserstoryService userstoryService, IEmployeeService employeeService)
        {
            this._projectService = projectService;
            this._userStoryService = userstoryService;
            this._employeeService = employeeService;
        }


        // GET: Project
        public ActionResult Index()
        {
            var projects = this._projectService.GetAll().Include(p => p.CreatedBy).Include(p => p.ModifiedBy).Include(p => p.ProductOwner);
            return View(projects.ToList());
        }

        // GET: Project/Details/5
        public ActionResult UserStories(int id)
        {
            Project project = this._projectService.GetById(id);

            if(project.userStoryList.Count==0)
            {
                project=new Project();
                project.userStoryList = new List<UserStory>();
            }     
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            //ViewBag.CreatedById = new SelectList(db.Employees, "EmployeeId", "Name");
            //ViewBag.ModifiedById = new SelectList(db.Employees, "EmployeeId", "Name");            
            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate,EmployeeId,CreatedById,ModifiedById,CreatedDate,ModifiedDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                this._projectService.Create(project);
                return RedirectToAction("Index");
            }

            //ViewBag.CreatedById = new SelectList(db.Employees, "EmployeeId", "Name", project.CreatedById);
            //ViewBag.ModifiedById = new SelectList(db.Employees, "EmployeeId", "Name", project.ModifiedById);
            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", project.EmployeeId);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            Project project =_projectService.GetById(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CreatedById = new SelectList(db.Employees, "EmployeeId", "Name", project.CreatedById);
            //ViewBag.ModifiedById = new SelectList(db.Employees, "EmployeeId", "Name", project.ModifiedById);
           
            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", project.EmployeeId);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate,EmployeeId,CreatedById,ModifiedById,CreatedDate,ModifiedDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Update(project);
                return RedirectToAction("Index");
            }
            //ViewBag.CreatedById = new SelectList(db.Employees, "EmployeeId", "Name", project.CreatedById);
            //ViewBag.ModifiedById = new SelectList(db.Employees, "EmployeeId", "Name", project.ModifiedById);
            //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", project.EmployeeId);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            Project project = _projectService.GetById(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project pr = _projectService.GetById(id);
            _projectService.Delete(pr);
            return RedirectToAction("Index");
        }
    }
}
