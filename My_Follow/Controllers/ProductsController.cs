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
using Microsoft.AspNet.Identity;
using System.Data.Entity.Infrastructure;

namespace My_Follow.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.Products.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}

            //var currentUser = User.Identity.Name;
            //return View();

            //var LoggedInUser = db.ProductOwners.FirstOrDefault(x => x.Email == currentUser);
            //if (Id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    // ViewBag.ProductOwnersID = Id;
            //}
            //Product product = db.Products.Find(Id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}





            var products = db.Products.Include(p => p.ProductOwners);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.Products.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //product.ProductOwnersID = id.Value;
         //   product.ProductID = id.Value;
            //Product product = new Product();
            //product.ProductOwnersID = 
               
           // ViewBag.ProductOwnersID = product.ProductOwnersID;
             // var LoggedInUser = db.ProductOwners.Where(x => x.ProductOwnersID == currentUserID).ToList();
            
             //var currentUser = User.Identity.Name;
            //ViewBag.ProductOwnersID = new SelectList(db.ProductOwners, "ProductOwnersID", "Email");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductName,Details")] Product product)
        {
            
            
          
            if (ModelState.IsValid)
            {

                var currentUser = User.Identity.Name;
                var LoggedInUser = db.ProductOwners.FirstOrDefault(u => u.Email == currentUser);
                product.ProductOwnersID = LoggedInUser.ProductOwnersID;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");

                return RedirectToAction("Products", "EndUsers");
               
               
            }

            ViewBag.ProductOwnersID = product.ProductOwnersID;
           
           // ViewBag.ProductOwnersID = new SelectList(db.ProductOwners, "ProductOwnersID", "Email", product.ProductOwnersID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(i => i.ProductOwners).Where(i => i.ProductID == id).Single();
            if (product == null)
            {
                return HttpNotFound();
            }
         //   ViewBag.ProductOwnersID = new SelectList(db.ProductOwners, "ProductOwnersID", "Email", product.ProductOwnersID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productsToUpdate = db.Products
               .Include(i => i.ProductOwners)
               .Where(i => i.ProductID == id)
               .Single();

            if (TryUpdateModel(productsToUpdate, "",
               new string[] { "ProductName", "Details"}))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(productsToUpdate.ProductOwners.Email))
                    {

                   
                        productsToUpdate.ProductOwners = null;
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(productsToUpdate);
        }


        //public ActionResult Edit(int? id,Product p )
        //{
        //    Product product = db.Products.Include(i => i.ProductOwners).Where(i => i.ProductID == id).Single();
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProductOwnersID = product.ProductOwnersID;
        //    return View(product);
        //}

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
 
        public ActionResult Redirect(int? id)
        {
            return RedirectToAction("Create", "Updates", new { id =id });
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
