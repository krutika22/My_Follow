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
using System.IO;
using My_Follow.CustomResult;

namespace My_Follow.Controllers
{
    public class MediaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        //[AllowAnonymous]
        //public ActionResult Upload(int Id)
        //{
        //    ViewBag.id = Id;
        //    return View();
        //}

        public ActionResult Index()
        {
            return View();
        }

        //  GET: Media
        public ActionResult Create()
        {

            return View();
            //var media = db.Media.Include(m => m.Updates);
            //return View(media.ToList());
        }

        [HttpPost]
        public ActionResult Create(Media media,int id)
        {

            foreach (var file in media.Images)
            {

                if(file.ContentLength>0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                    file.SaveAs(path);
                    ViewBag.Path = path;
                }

                Media m = new Media()
                {

                    UpdatesID = id,
                    Images = media.Images,
                    Video = media.Video



                };

                db.Media.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = media.UpdatesID });

               

            }

         


            return View(media);
        }


     

        //public ActionResult Index()
        //{
        //    return new VideoResult();
        //}


        public ActionResult Uploads()
        {
            return View();
       
        }


            //Media m = new Media();

            //   // UpdatesID = m.UpdatesID,
            //    Image = m.Image,
            //    Video = m.Video
               
            //};
            //int times = db.Media.Where(d => d.UpdatesID == MediaID).Count();


            //if (times < 5)
            //{

            //for (int i = 0; i < 6; i++)
            //{

            
                /*Lopp for multiple files*/
              
                    /*Geting the file name*/
                   // string filename = System.IO.Path.GetFileName(file.FileName);
                    /*Saving the file in server folder*/
               //     file.SaveAs(Server.MapPath("~/Images/" + filename));
                 //   string filepathtosave = "Images/" + filename;
                    /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/


                   
                //    db.Media.Add(m);
                  //  db.SaveChanges();
             //   }

                //ViewBag.Message = "Media Uploaded successfully";
            //}


            //else{
            //    ViewBag.Message = "Cannot upload more than 5 Media";
            //}

          



            // GET: Media/Details/5
            //public ActionResult Details(int? id)
            //{
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Media media = db.Media.Find(id);
            //    if (media == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(media);
            //}

            // GET: Media/Create
        //public ActionResult Create()
        //{
        //    ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro");
        //    return View();
        //}

            // POST: Media/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Create([Bind(Include = "MediaID,Image,Video,UpdatesID")] Media media)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Media.Add(media);
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }

            //    ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro", media.UpdatesID);
            //    return View(media);
            //}

            // GET: Media/Edit/5
            //public ActionResult Edit(int? id)
            //{
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Media media = db.Media.Find(id);
            //    if (media == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro", media.UpdatesID);
            //    return View(media);
            //}

            // POST: Media/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Edit([Bind(Include = "MediaID,Image,Video,UpdatesID")] Media media)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Entry(media).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    ViewBag.UpdatesID = new SelectList(db.Updates, "UpdatesID", "Intro", media.UpdatesID);
            //    return View(media);
            //}

            // GET: Media/Delete/5
            //public ActionResult Delete(int? id)
            //{
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Media media = db.Media.Find(id);
            //    if (media == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(media);
            //}

            // POST: Media/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public ActionResult DeleteConfirmed(int id)
            //{
            //    Media media = db.Media.Find(id);
            //    db.Media.Remove(media);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //protected override void Dispose(bool disposing)
            //{
            //    if (disposing)
            //    {
            //        db.Dispose();
            //    }
            //    base.Dispose(disposing);
            //}


      

        }
    
}

