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
    public class HistryVisitsController : Controller
    {
        private WetContext db = new WetContext();

        // GET: HistryVisits
        public ActionResult Index()
        {
            var historyVisits = db.HistoryVisits.Include(h => h.Zwierzak);
            return View(historyVisits.ToList());
        }

        // GET: HistryVisits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistryVisit histryVisit = db.HistoryVisits.Find(id);
            if (histryVisit == null)
            {
                return HttpNotFound();
            }
            return View(histryVisit);
        }

        // GET: HistryVisits/Create
        public ActionResult Create()
        {
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name");
            return View();
        }

        // POST: HistryVisits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VisitDate,Description,AnimalID")] HistryVisit histryVisit)
        {  
            if (ModelState.IsValid)
            {
                db.HistoryVisits.Add(histryVisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name", histryVisit.AnimalID);
            return View(histryVisit);
        }

        // GET: HistryVisits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistryVisit histryVisit = db.HistoryVisits.Find(id);
            if (histryVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name", histryVisit.AnimalID);
            return View(histryVisit);
        }

        // POST: HistryVisits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VisitDate,Description,AnimalID")] HistryVisit histryVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(histryVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name", histryVisit.AnimalID);
            return View(histryVisit);
        }

        // GET: HistryVisits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistryVisit histryVisit = db.HistoryVisits.Find(id);
            if (histryVisit == null)
            {
                return HttpNotFound();
            }
            return View(histryVisit);
        }

        // POST: HistryVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistryVisit histryVisit = db.HistoryVisits.Find(id);
            db.HistoryVisits.Remove(histryVisit);
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
