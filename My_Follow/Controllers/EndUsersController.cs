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
using My_Follow.ViewModel;
using System.Data.Entity.Infrastructure;


namespace My_Follow.Controllers
{
    public class EndUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EndUsers
        public ActionResult Index(int? id, int? ProductID)
        {

            //var p = endusers.Products.SingleOrDefault(c => c.ProductID == ProductID);
            //    //var e = p.EndUsers.SingleOrDefault(i => i.EndUsersID == EndUsersID);
            //    //if (e == null)
            //    //    p.EndUsers.Add(endusers.EndUsers.Single(i => i.EndUsersID == EndUsersID));

            //    return RedirectToAction("Products", "Products");
            //    return View(db.Products.ToList());

            //var endusers = db.EndUsers.Include(p => p.Products.Select(c => c.ProductOwners));
            //return View(endusers.ToList());


            var viewModel = new EndUsersIndexData();
            viewModel.EndUsers = db.EndUsers.Include(p => p.Products.Select(c => c.ProductOwners));

            if (id != null)
            {
                ViewBag.EndUsersID = id.Value;
                viewModel.Products = viewModel.EndUsers.Where(i => i.EndUsersID == id.Value).Single().Products;
            }

            //if (ProductID != null)
            //{
            //    ViewBag.ProductID = ProductID.Value;
            //    viewModel.ProductOwners = viewModel.Products.Where(
            //        x => x.ProductID == ProductID).Select(c => c.ProductOwners);
            //}

            return View(viewModel);

            //if (id != null)
            //{
            //    EndUsersID = id;
            //    Products = endusers.Where(i => i.EndUsersID == id.Value).Single().Products;
            //}






          //  return View(db.EndUsers.ToList());
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
        public ActionResult Products()
        {
          //  EndUsers endUsers = new EndUsers();
            //var p = db.Products.Include(p => p.ProductID);
            //var e = p.EndUsers.SingleOrDefault(i => i.EndUsersID == EndUsersID);
            //if (e == null)
            //    p.EndUsers.Add(endusers.EndUsers.Single(i => i.EndUsersID == EndUsersID));
         //  PopulateFollowedProducts(endUsers);
            return View(db.Products.ToList());

           
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Products(ApplicationDbContext context,EndUsers endUsers, int? id)
        {
            var currentUser = User.Identity.Name;
           var LoggedInUser = db.Users.Where(u => u.Email == currentUser);
            //product.ProductOwnersID = LoggedInUser.ProductOwnersID;
   

           // var p = context.Products.SingleOrDefault(c => c.ProductID == id);

           //var e = p.EndUsers.SingleOrDefault(i => i.Email == currentUser);



           //if (e == null)


           PopulateFollowedProducts(endUsers);
             




               //  p.EndUsers.Add(context.EndUsers.Single(i => i.EndUsersID == endUsers));
         //   var e = p.EndUsers.SingleOrDefault(i => i.Email == (e)IQueryable<ApplicationUser>LoggedInUser);

          //  var endusersproducts = e.Products;
        

        //    var e = p.EndUsers.SingleOrDefault(i => i.EndUsersID == EndUsersID);
       //     if (e == null)
        //        p.EndUsers.Add(context.EndUsers.Single(i => i.Name == EndUsersName));


       //if (userAction == "+ Follow")
       //{
       //    ViewBag.SubmitValue = "+ Follow";
       // }

       //if (userAction == "+ Unfollow") 
       // {
       //     ViewBag.SubmitValue = "+ Unfollow";
       // }

            if (ModelState.IsValid)
            {
                EndUsers eu = new EndUsers()
                {
                    EndUsersID = endUsers.EndUsersID


                };

                db.EndUsers.Add(eu);
              //  db.SaveChanges();
             //   return RedirectToAction("Index");
            }

            return View(endUsers);
        }


         //   EndUsers eu = new EndUsers()
         //   {
         //     //  EndUsersID = endusers.EndUsersID
              

         //   };


         //   db.EndUsers.Add(eu);
         //   db.SaveChanges();
         ////   return RedirectToAction("Products", new { id = endusers.EndUsersID});



        






        

        //public ActionResult Products()
        //{
        //    //var p = endusers.Products.SingleOrDefault(c => c.ProductID == ProductID);
        //    //var e = p.EndUsers.SingleOrDefault(i => i.EndUsersID == EndUsersID);
        //    //if (e == null)
        //    //    p.EndUsers.Add(endusers.EndUsers.Single(i => i.EndUsersID == EndUsersID));

        //    return RedirectToAction("Products", "Products");
        //    return View(db.Products.ToList());


        //}
       
        //public ActionResult Products(EndUsers endusers, int id)
     
        
        
        
        

        //    {
        //        EndUsersID = id

        //    };
        //    db.EndUsers.Add(e);
        //    db.SaveChanges();
        //    return RedirectToAction("", new { id = endusers.EndUsersID });
        //    return View(endusers);
        //    //var products = db.Products.Include(p => p.ProductOwners);
        //  return View(db.Products.ToList());

        //}

        // GET: EndUsers/Create
        public ActionResult Create()
        {
            var endUser = new EndUsers();
            endUser.Products= new List<Product>();
            PopulateFollowedProducts(endUser);
            return View();
        }

        // POST: EndUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EndUsersID,Name,Email,DateOfJoining,Gender,DateOfBirth,Street1,Street2,City,State,Country,Pin,ContactNumber")] EndUsers endUsers, string[] followedProducts)
        {
            if (followedProducts != null)
            {
                endUsers.Products = new List<Product>();
                foreach (var product in followedProducts)
                {
                    var productToAdd = db.Products.Find(int.Parse(product));
                    endUsers.Products.Add(productToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.EndUsers.Add(endUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateFollowedProducts(endUsers);
            return View(endUsers);
        }





        // GET: EndUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EndUsers endUsers = db.EndUsers.Include(i => i.Products).Where(i => i.EndUsersID == id).Single();
            PopulateFollowedProducts(endUsers);
            if (endUsers == null)
            {
                return HttpNotFound();
            }
            return View(endUsers);
        }

        // POST: EndUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] followedProducts)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var endUserToUpdate = db.EndUsers
               .Include(i => i.Products)
               .Where(i => i.EndUsersID == id)
               .Single();

            if (TryUpdateModel(endUserToUpdate, "",
               new string[] { "Email", "DateOfJoining", "Gender", "DateOfBirth", "Address","Name"}))
            {
                try
                {

                    UpdateFollowers(followedProducts, endUserToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateFollowedProducts(endUserToUpdate);
            return View(endUserToUpdate);
        }



        //public ActionResult Edit([Bind(Include = "EndUsersID,Name,Email,DateOfJoining,Gender,DateOfBirth,Street1,Street2,City,State,Country,Pin,ContactNumber")] EndUsers endUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(endUsers).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(endUsers);
        //}

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





        private void PopulateFollowedProducts(EndUsers endUsers)
        {
            var allProducts = db.Products;
            var followers = new HashSet<int>(endUsers.Products.Select(c => c.ProductID));
            var viewModel = new List<FollowedProducts>();
            foreach (var product in allProducts)
            {
                viewModel.Add(new FollowedProducts
                {
                    ProductID = product.ProductID,
                    Followed = followers.Contains(product.ProductID)
                });
            }
            ViewBag.Products = viewModel;
        }

        private void UpdateFollowers(string[] followedProducts, EndUsers endUserToUpdate)
        {
            if (followedProducts == null)
            {
                endUserToUpdate.Products = new List<Product>();
                return;
            }

            var followedProductsHS = new HashSet<string>(followedProducts);
            var followers = new HashSet<int>
                (endUserToUpdate.Products.Select(c => c.ProductID));
            foreach (var product in db.Products)
            {
                if (followedProductsHS.Contains(product.ProductID.ToString()))
                {
                    if (!followers.Contains(product.ProductID))
                    {
                        endUserToUpdate.Products.Add(product);
                    }
                }
                else
                {
                    if (followers.Contains(product.ProductID))
                    {
                        endUserToUpdate.Products.Remove(product);
                    }
                }
            }
        }



        public ActionResult Redirect(int? ProductID)
        {

            return RedirectToAction("Browse", "Updates", new { ProductID = ProductID });
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
