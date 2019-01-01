using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Weterzynarze.DAL;
using Weterzynarze.Models;

namespace Weterzynarze.Controllers
{
    public class HealthCardsController : Controller
    {
        private WetContext db = new WetContext();

        // GET: HealthCards
        public ActionResult Index()
        {
            var healthCards = db.HealthCards.Include(h => h.Animal);
            return View(healthCards.ToList());
        }

        // GET: HealthCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCard healthCard = db.HealthCards.Find(id);
            if (healthCard == null)
            {
                return HttpNotFound();
            }
            return View(healthCard);
        }

        // GET: HealthCards/Create
        public ActionResult Create(int Animal_ID)
        {


            var item = db.Animals.Where(x => x.ID == Animal_ID ).First();

            var data = new SelectList(db.Animals, "ID", "Name", item).First();

            List<SelectListItem> lists = new List<SelectListItem>
            {
                data
            };

            ViewBag.AnimalID = lists;

            return View();
        }

        // POST: HealthCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreateTime,Description,Treatment,AnimalID")] HealthCard healthCard)
        {
            
     
            if (ModelState.IsValid)
            {
            var animals = db.Animals.Where(x => x.ID == healthCard.AnimalID).First();
              healthCard.Animal = animals;
                db.HealthCards.Add(healthCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimalID = new SelectList(db.Animals, "ID", "Name");
            return View(healthCard);
        }

        // GET: HealthCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCard healthCard = db.HealthCards.Find(id);
            if (healthCard == null)
            {
                return HttpNotFound();
            }
    
            return View(healthCard);
        }

        // POST: HealthCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreateTime,Description,Treatment,AnimalID")] HealthCard healthCard)
        {
            if (ModelState.IsValid)
            {
          
                db.Entry(healthCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(healthCard);
        }

        // GET: HealthCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCard healthCard = db.HealthCards.Find(id);
            if (healthCard == null)
            {
                return HttpNotFound();
            }
            return View(healthCard);
        }

        // POST: HealthCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HealthCard healthCard = db.HealthCards.Find(id);
            db.HealthCards.Remove(healthCard);
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
