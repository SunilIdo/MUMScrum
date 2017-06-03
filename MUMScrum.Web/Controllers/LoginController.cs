using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MUMScrum.Model.ViewModel;
using MUMScrum.Infrastructure.Utility;
using System.Web.Security;
using MUMScrum.Model.SessionModel;
using MUMScrum.Web.Models;
using MUMScrum.Data;
namespace MUMScrum.Web.Controllers
{
    public class LoginController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginObj)
        {
            if (loginObj.Password != null || loginObj.UserName != null)
            {
                ViewBag.ErrorMessage = "";
                var password = Base64.Encrypt(loginObj.Password);
                var loginUser = db.LoginUsers.Where(x => x.UserName.ToLower() == loginObj.UserName.ToLower() && x.Password == password).FirstOrDefault();
                if (loginUser != null)
                {
                    FormsAuthentication.SetAuthCookie(loginObj.UserName, loginObj.RememberMe);
                    SessionModel.LoginUser = loginUser;
                    return RedirectToAction("Index", "Project");
                }
                else
                {
                    ViewBag.ErrorMessage = "Username and password does'nt match.";
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Username and password is required.";
            }

            return View(loginObj);
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
