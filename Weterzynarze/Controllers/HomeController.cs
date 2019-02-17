using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weterzynarze.DAL;
using Weterzynarze.Models;
using Weterzynarze.ViewModels;


namespace Weterzynarze.Controllers
{
    public class HomeController : Controller
    {
        private WetContext db = new WetContext();
        public ActionResult Index()
        {
            return View(db.Services.ToList());
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