using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MUMScrum.Model.Model;
using MUMScrum.Model.Model;
using MUMScrum.Infrastructure.Utility;
using MUMScrum.Data;
using MUMScrum.Service;
using MUMScrum.Service.Service.Users;

namespace MUMScrum.Web.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService;
        }
        // GET: /Users/
        public ActionResult Index()
        {

            List<LoginUser> users = _usersService.GetAll().Include("Employee").ToList();
            //List<LoginUser> users = db.LoginUsers.Include("Employee").ToList();
            List<LoginUser> usersNew = new List<LoginUser>();
            foreach (LoginUser user in users) 
            {
                user.Password = Base64.Decrypt(user.Password);
                usersNew.Add(user);
            }
            return View(usersNew);
        }

        // GET: /Users/Details/5
        public ActionResult Details(int id)
        {
            LoginUser loginuser = _usersService.GetById(id);
            if (loginuser == null)
            {
                return HttpNotFound();
            }
            return View(loginuser);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.Employees = new SelectList(db.Employees, "EmployeeId", "Name");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserName,Password,EmployeeId,RoleId")] LoginUser loginuser)
        {
            ViewBag.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                var UserNameCheckIfAlreadyExists = db.LoginUsers.Where(x => x.UserName.ToLower() == loginuser.UserName.ToLower()).FirstOrDefault();
                if (UserNameCheckIfAlreadyExists == null) 
                {

                    loginuser.Password = Base64.Encrypt(loginuser.Password);
                    _usersService.Create(loginuser);
                    return RedirectToAction("Index");
                }
               
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", loginuser.EmployeeId);
            ViewBag.ErrorMessage = "User name already exists.Please try another one.";
            return View(loginuser);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginUser loginuser = _usersService.GetById(id);
            loginuser.Password = Base64.Decrypt(loginuser.Password);
            if (loginuser == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", loginuser.EmployeeId);
            return View(loginuser);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserName,Password,EmployeeId,RoleId")] LoginUser loginuser)
        {
            ViewBag.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                 var UserNameCheckIfAlreadyExists = db.LoginUsers.Where(x => x.UserName.ToLower() == loginuser.UserName.ToLower()).FirstOrDefault();
                 if (UserNameCheckIfAlreadyExists == null)
                 {
                     loginuser.Password = Base64.Encrypt(loginuser.Password);
                     _usersService.Update(loginuser);
                     db.Entry(loginuser).State = EntityState.Modified;
                     db.SaveChanges();
                     return RedirectToAction("Index");
                 }
            }
            ViewBag.ErrorMessage = "User name already exists.Please try another one."; ;
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", loginuser.EmployeeId);
            return View(loginuser);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginUser loginuser = db.LoginUsers.Find(id);
            if (loginuser == null)
            {
                return HttpNotFound();
            }
            return View(loginuser);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginUser loginuser = db.LoginUsers.Find(id);
            db.LoginUsers.Remove(loginuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
