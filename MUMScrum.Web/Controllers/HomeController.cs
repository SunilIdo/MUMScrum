using MUMScrum.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MUMScrum.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProjectService _projectService;

        public HomeController(IProjectService projectService)
        {
            this._projectService = projectService;
        }
        public ActionResult Index()
        {
            var test = _projectService.GetAll();
            return View(_projectService.GetAll().Include(p => p.ProductOwner));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}