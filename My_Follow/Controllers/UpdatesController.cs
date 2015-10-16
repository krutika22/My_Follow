using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using My_Follow.Models;

namespace My_Follow.Controllers
{
    public class UpdatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Updates
        public ActionResult Index()
        {
            return View(db.Updates.ToList());
        }

        // GET: Updates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Updates updates = db.Updates.Find(id);
            if (updates == null)
            {
                return HttpNotFound();
            }
            return View(updates);
        }

        // GET: Updates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Updates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UpdatesID,Intro,Details,Photo")] Updates updates)
        {
            if (ModelState.IsValid)
            {
                db.Updates.Add(updates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(updates);
        }

        // GET: Updates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Updates updates = db.Updates.Find(id);
            if (updates == null)
            {
                return HttpNotFound();
            }
            return View(updates);
        }

        // POST: Updates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UpdatesID,Intro,Details,Photo")] Updates updates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(updates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updates);
        }

        // GET: Updates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Updates updates = db.Updates.Find(id);
            if (updates == null)
            {
                return HttpNotFound();
            }
            return View(updates);
        }

        // POST: Updates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Updates updates = db.Updates.Find(id);
            db.Updates.Remove(updates);
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
