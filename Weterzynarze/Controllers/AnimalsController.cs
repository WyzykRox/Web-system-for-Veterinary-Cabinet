using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public static void SendMail(string to, string body, string subject)
        {
            var message = new System.Net.Mail.MailMessage(ConfigurationManager.AppSettings["sender"], to)
            {
                Subject = subject,
                Body = body
            };
            var smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = ConfigurationManager.AppSettings["smtpHost"],
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["sender"],
                    ConfigurationManager.AppSettings["passwd"]),
                EnableSsl = true,
                Port = 587
            };
            smtpClient.Send(message);
        }


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
            return View(db.Animals.Where(_ => _.Owner.Email == user.Email).ToList());

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
            ViewBag.Race = new SelectList(db.Races, "ID", "Name");
            ViewBag.plec = new SelectList(plec, plec);
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Colour,Sex,DistinuishingMarks,RaceID,ChipId,Picture,Created,grafting")] Animal animal)
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
                var race = db.Races.Find(animal.RaceID);
                animal.Rasa = race;
                var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                var user = userManager.FindByName(User.Identity.Name);
                animal.Owner = db.Profiles.SingleOrDefault(_ => _.Email == user.Email);
                db.Animals.Add(animal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Race = new SelectList(db.Races, "ID", "Name");
            return View(animal);
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.plec = new SelectList(plec,plec);
            ViewBag.Race = new SelectList(db.Races, "ID", "Name");
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
        public ActionResult Edit([Bind(Include = "ID,Name,Colour,Sex,DistinuishingMarks,RaceID,ChipId,Picture,Created,grafting")] Animal animal)
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
                var race = db.Races.Find(animal.RaceID);
                animal.Rasa = race;
                db.Entry(animal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Animals");
            }
            ViewBag.Race = new SelectList(db.Races, "ID", "Name");
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
        private string[] plec { get; } = {
            "samiec",
            "samica",
            "bezpłuciowiec"
        };
    }
}
