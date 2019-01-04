using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PagedList;
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
    public class ProfilesController : Controller
    {
        private WetContext db = new WetContext();

        // GET: Profiles
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var profiles = from s in db.Profiles
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)
                                       || s.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    profiles = profiles.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    profiles = profiles.OrderBy(s => s.Created);
                    break;
                case "date_desc":
                    profiles = profiles.OrderByDescending(s => s.Created);
                    break;
                default:
                    profiles = profiles.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(profiles.ToPagedList(pageNumber, pageSize));
        }

        // GET: Profiles/MyDetails/5
        public ActionResult MyDetails()
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var user = userManager.FindByName(User.Identity.Name);
            var prof = db.Profiles.Where(_ => _.Email == user.Email).First();
            Profile profile = prof;
            return View(profile);
        }

        // GET: Profiles/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profiles/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "ID,Email,FirstName,LastName,Created")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit([Bind(Include = "ID,Email,FirstName,LastName,Created")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
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

        public String GetProfile(String email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest).ToString();
            }
            Profile profile = db.Profiles.Single(element => element.Email == email);
            if (profile == null)
            {
                return HttpNotFound().ToString();
            }
            return JsonConvert.SerializeObject(profile, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
        }

    }
}
