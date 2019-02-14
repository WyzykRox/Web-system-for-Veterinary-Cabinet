using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

        private WetContext db = new WetContext();

        // GET: Visits
        public ActionResult Index()
        {
            var visits = db.Visits.Include(v => v.Zwierzak);
            return View(visits.ToList());
        }

        // GET: ToDayvisits
        public ActionResult ToDayvisits() 
        {
            DateTime time = DateTime.Now;
           // var visits = db.Visits.Include(v => v.Zwierzak).ToList();
            //visits = visits.Where(_ => _.VisitDate.Date.Year == time.Date.Year && _.VisitDate.Date.Month == time.Date.Month && _.VisitDate.Date.Day == time.Date.Day).ToList();
            var visits = db.Visits.Include(v => v.Zwierzak).Where(_ => DbFunctions.TruncateTime(_.VisitDate) == time.Date).ToList();

            
            return View(visits);
        }

        // GET: My Visits
        public ActionResult MyVisits()
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var user = userManager.FindByName(User.Identity.Name);


            var visits = db.Visits.Where(_ => _.User.Email == user.Email).Include(v => v.Zwierzak);
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
            int spr = Enumerable.Count(ViewBag.AnimalID);
            if (spr == 0)
            {
                return RedirectToAction("Create", "Animals");
            }
            return View();
        }

        // POST: Visits/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VisitDate,Description,AnimalID")] Visit visit)
        {
            
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var user = userManager.FindByName(User.Identity.Name);

            DateTime date;
            date = db.Visits.Where(_ => _.VisitDate == visit.VisitDate).Select(_ => _.VisitDate).FirstOrDefault();
            if ( date == visit.VisitDate)
            {
                date = db.Visits.Where(_ => _.VisitDate == visit.VisitDate).Select(_ => _.VisitDate).First();
            }else date = new DateTime();

           int result = DateTime.Compare(date, visit.VisitDate);


            if (ModelState.IsValid && result != 0 && (visit.VisitDate.Hour >= 8)== true && (visit.VisitDate.Hour < 16)==true)
            {
             
                visit.User = db.Profiles.SingleOrDefault(_ => _.Email == user.Email);
                db.Visits.Add(visit);
                db.SaveChanges();
                SendMail(User.Identity.Name, "Dziekujemy za zapisanie się na wizytę dnia: " + visit.VisitDate, " Powiadomienie z gabinetu Gab wet");
                return RedirectToAction("Index","Home");
            }
            string noResult = "Data zajęta wybierz inną";
            ViewBag.Message = noResult;
 
            ViewBag.AnimalID = new SelectList(db.Animals.Where(_ => _.Owner.Email == user.Email), "ID", "Name");
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
            double doWiz = visit.VisitDate.Date.Subtract(DateTime.Now.Date).TotalDays;
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
