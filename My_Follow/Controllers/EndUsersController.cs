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
    public class EndUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EndUsers
        public ActionResult Index()
        {
            var endUsers = db.EndUsers.Include(e => e.Product).Include(e => e.Updates);
            return View(endUsers.ToList());
        }

        // GET: EndUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EndUsers endUsers = db.EndUsers.Find(id);
            if (endUsers == null)
            {
                return HttpNotFound();
            }
            return View(endUsers);
        }

        // GET: EndUsers/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro");
            return View();
        }

        // POST: EndUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EndUsersID,UpdatesID,ProductID,Email,DateOfJoining,Gender,DateOfBirth,Street1,Street2,City,State,Country,Pin,ContactNumber")] EndUsers endUsers)
        {
            if (ModelState.IsValid)
            {
                db.EndUsers.Add(endUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", endUsers.ProductID);
            ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro", endUsers.UpdatesID);
            return View(endUsers);
        }

        // GET: EndUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EndUsers endUsers = db.EndUsers.Find(id);
            if (endUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", endUsers.ProductID);
            ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro", endUsers.UpdatesID);
            return View(endUsers);
        }

        // POST: EndUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EndUsersID,UpdatesID,ProductID,Email,DateOfJoining,Gender,DateOfBirth,Street1,Street2,City,State,Country,Pin,ContactNumber")] EndUsers endUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", endUsers.ProductID);
            ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro", endUsers.UpdatesID);
            return View(endUsers);
        }

        // GET: EndUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EndUsers endUsers = db.EndUsers.Find(id);
            if (endUsers == null)
            {
                return HttpNotFound();
            }
            return View(endUsers);
        }

        // POST: EndUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EndUsers endUsers = db.EndUsers.Find(id);
            db.EndUsers.Remove(endUsers);
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
