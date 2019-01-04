using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Weterzynarze.DAL;
using Weterzynarze.Models;

namespace Weterzynarze.Controllers
{
    public class VisitsController : Controller
    {
        private WetContext db = new WetContext();

        // GET: Visits
        public ActionResult Index()
        {
            var visits = db.Visits.Include(v => v.Zwierzak);
            return View(visits.ToList());
        }

        // GET: Visits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // GET: Visits/Create
        public ActionResult Create()
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var user = userManager.FindByName(User.Identity.Name);
            ViewBag.AnimalID = new SelectList(db.Animals.Where(_ => _.Owner.Email == user.Email), "ID", "Name");
            return View();
        }

        // POST: Visits/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VisitDate,Description,AnimalID")] Visit visit)
        {
            DateTime date = db.Visits.Where(_ => _.VisitDate == visit.VisitDate).Select(_ => _.VisitDate).First();

           int result = DateTime.Compare(date, visit.VisitDate);
            if (ModelState.IsValid && result != 0)
            {
                var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                var user = userManager.FindByName(User.Identity.Name);
                visit.User = db.Profiles.SingleOrDefault(_ => _.Email == user.Email);
                db.Visits.Add(visit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name");
            return View(visit);
        }

        // GET: Visits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var visit = db.Visits.SingleOrDefault(_ => _.ID == id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name");
            return View(visit);
        }

        // POST: Visits/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VisitDate,Description,AnimalID")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name", visit.AnimalID);
            return View(visit);
        }

        // GET: Visits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visit visit = db.Visits.Find(id);
            db.Visits.Remove(visit);
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
