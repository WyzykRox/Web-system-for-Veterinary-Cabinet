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
using Weterzynarze.ViewModels;


namespace Weterzynarze.Controllers
{
    public class AnimalsController : Controller
    {
        private WetContext db = new WetContext();

        // GET: ShowAll
        public ActionResult ShowAll()
        {
            return View(db.Animals.ToList());
        }

        // GET: Animals
        public ActionResult Index()
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var user = userManager.FindByName(User.Identity.Name);
            return View(db.Animals.Where(_ => _.Owner.Email == user.Email));

        }

        // GET: Animals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Colour,Sex,Breed,DistinuishingMarks,ChipId,Picture,Created")] Animal animal)
        {


            if (ModelState.IsValid)
            {

                HttpPostedFileBase file = Request.Files["Obrazki"];
                if (file != null && file.ContentLength > 0)
                {

                    animal.Picture = file.FileName;
                    string path = (HttpContext.Server.MapPath("~/Picture/") + animal.Picture);
                    file.SaveAs(path);
                }

                var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                var user = userManager.FindByName(User.Identity.Name);
                animal.Owner = db.Profiles.SingleOrDefault(_ => _.Email == user.Email);
                db.Animals.Add(animal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(animal);
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Colour,Sex,Breed,DistinuishingMarks,ChipId,Picture,Created")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animal animal = db.Animals.Find(id);
            db.Animals.Remove(animal);
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
