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

namespace MUMScrum.Web.Controllers
{
    public class ReleaseBacklogController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IReleaseBacklogService _releaseBacklogService;
        private IEmployeeService _employeeService;
        private IProjectService _projectService;

        public ReleaseBacklogController(IReleaseBacklogService releaseBacklogService,
            IEmployeeService employeeService, 
            IProjectService projectService)
        {
            this._releaseBacklogService = releaseBacklogService;
            this._employeeService = employeeService;
            this._projectService = projectService;
        }

        // GET: ReleaseBacklog
        public ActionResult Index()
        {
            var releaseBacklogs =_releaseBacklogService.GetAll().Include(r=>r.CreatedBy).Include(r=>r.ModifiedBy).Include(r => r.project);
            return View(releaseBacklogs.ToList());
        }

        // GET: ReleaseBacklog/Details/5
        public ActionResult Details(int id)
        {
            ReleaseBacklog releaseBacklog = _releaseBacklogService.GetById(id);
            if (releaseBacklog == null)
            {
                return HttpNotFound();
            }
            return View(releaseBacklog);
        }

        // GET: ReleaseBacklog/Create
        public ActionResult Create()
        {

            ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name");
            ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(),"EmployeeId", "Name");
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: ReleaseBacklog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ProjectId,CreatedById,ModifiedById,CreatedDate,ModifiedDate")] ReleaseBacklog releaseBacklog)
        {
            if (ModelState.IsValid)
            {
               _releaseBacklogService.Create(releaseBacklog);                
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", releaseBacklog.CreatedById);
            ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", releaseBacklog.ModifiedById);
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name", releaseBacklog.ProjectId);
            return View(releaseBacklog);
        }

        // GET: ReleaseBacklog/Edit/5
        public ActionResult Edit(int id)
        {         
            ReleaseBacklog releaseBacklog = _releaseBacklogService.GetById(id);
            if (releaseBacklog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", releaseBacklog.CreatedById);
            ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", releaseBacklog.ModifiedById);
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name", releaseBacklog.ProjectId);
            return View(releaseBacklog);
        }

        // POST: ReleaseBacklog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ProjectId,CreatedById,ModifiedById,CreatedDate,ModifiedDate")] ReleaseBacklog releaseBacklog)
        {
            if (ModelState.IsValid)
            {
                _releaseBacklogService.Update(releaseBacklog);            
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", releaseBacklog.CreatedById);
            ViewBag.ModifiedById = new SelectList(_employeeService.GetAll(), "EmployeeId", "Name", releaseBacklog.ModifiedById);
            ViewBag.ProjectId = new SelectList(_projectService.GetAll(), "Id", "Name", releaseBacklog.ProjectId);
            return View(releaseBacklog);
        }

        // GET: ReleaseBacklog/Delete/5
        public ActionResult Delete(int id)
        {            
            ReleaseBacklog releaseBacklog =_releaseBacklogService.GetById(id);
            if (releaseBacklog == null)
            {
                return HttpNotFound();
            }
            return View(releaseBacklog);
        }

        // POST: ReleaseBacklog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReleaseBacklog releaseBacklog = _releaseBacklogService.GetById(id);
            _releaseBacklogService.Delete(releaseBacklog);            
            return RedirectToAction("Index");
        }
        public ActionResult AssignUStoReleaseBacklog()
        {
            ViewBag.ReleaseBacklogId = new SelectList(_releaseBacklogService.GetAll(), "Id","Name");
            return View();
        }
    }
}
