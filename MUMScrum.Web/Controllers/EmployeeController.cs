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
using MUMScrum.Model.ENUMS;

namespace MUMScrum.Web.Controllers
{
    public class EmployeeController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IEmployeeService _employeeService;
        private IUserstoryService _userstoryService;

        public EmployeeController(IEmployeeService employeeService, IUserstoryService userstoryService)
        {
            this._employeeService = employeeService;
            this._userstoryService = userstoryService;
        }

        // GET: Employee
        [MUMScrum.Infrastructure.Utility.CustomAuthorization.AuthorizeUser(Role = RoleEnum.ProductOwner)]
        public ActionResult Index()
        {
            return View(_employeeService.GetAll());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetUserstoryByDevId(int id)
        {
            return View(_userstoryService.GetUserstoryByDevId(id));
        }
        public ActionResult GetUserstoryByTesterId(int id)
        {
            return View(_userstoryService.GetUserstoryByTesterId(id));
        }
        public ActionResult UpdateDevelopmentEffort(int id)
        {
            return View(_userstoryService.GetUserstoryByTesterId(id));
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [MUMScrum.Infrastructure.Utility.CustomAuthorization.AuthorizeUser(Role=RoleEnum.ScrumMaster)]
        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,Name,DateOfBirth,Address,City,State,ZipCode,Phone,Email,Status,Sex,Salary,Bonus,Commission,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Create(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee =_employeeService.GetById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,Name,DateOfBirth,Address,City,State,ZipCode,Phone,Email,Status,Sex,Salary,Bonus,Commission,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Update(employee);                
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee =_employeeService.GetById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee =_employeeService.GetById(id);
            _employeeService.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
