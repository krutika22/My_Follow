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
using System.Data.Entity.Infrastructure;


namespace My_Follow.Controllers
{
    public class UpdatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
   

        // GET: Updates
        public ActionResult Index()
        {

           
        var updates = db.Updates.Include(u => u.Products);
        
        return View(updates.ToList());

        //if (ProductID != null)
        //{
        //    ViewBag.ProductID = ProductID.Value;
        //}
       
            //if (ProductID != null)
            //{
            //    ViewBag.ProductID = ProductID.Value;
            //    viewModel.Updates = viewModel.Product.Where(
            //x => x.ProductID == ProductID).Single().Updates;

            //}
        
            ////

            //return (viewModel);
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

            
            
     

            // ViewBag.ProductID = ID.Value;
          //  ViewBag.ProductID = id;
        
            //ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: Updates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Intro,Details")] Updates updates,int id)
        {
            //updates.Products = new Product();
            //updates.Products.ProductName = "";
            //try

            //{

            if (ModelState.IsValid)
            {
            Updates u = new Updates()
            {
                ProductID = id,
                Intro = updates.Intro,
                Details = updates.Details

            };
           

               
                    db.Updates.Add(u);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = updates.ProductID });
                }
            //   ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", updates.ProductID);
           //     ViewBag.ProductID = updates.ProductID;
                return View(updates);
        }

            
            
            //}
            //catch (Exception exception)
            //{
            //    //Log the error (uncomment dex variable name and add a line here to write a log.
            //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            //}

           

         //  
     
        // GET: Updates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Updates updates = db.Updates.Include(i => i.Products).Where(i => i.UpdatesID == id).Single();
            if (updates == null)
            {
                return HttpNotFound();
            }
         //   ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", updates.ProductID);
            return View(updates);
        }

        // POST: Updates/Edit/5
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
            var updatesToUpdate = db.Updates
               .Include(i => i.Products)
               .Where(i => i.UpdatesID == id)
               .Single();

            if (TryUpdateModel(updatesToUpdate, "",
               new string[] { "Intro ", "Details" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(updatesToUpdate.Products.Details))
                    {


                        updatesToUpdate.Products = null;
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
            return View(updatesToUpdate);
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


        public ActionResult Browse(){

            //var updates = db.Updates.Include(i => i.Products);
            return View();
        }

        public ActionResult Redirect(int? id)
        {
            return RedirectToAction("Create", "Media", new { id = id });
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




























