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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Mail;

using System.Web.UI;
using System.Security.Cryptography;
using System.Text;



namespace My_Follow.Controllers
{
    public class InvitationFormController : Controller
    {

        private ApplicationUserManager _userManager;
        public InvitationFormController() 
        {

        }




        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
       
              
        // GET: InvitationForm

       
        public ActionResult Index()
        {
      
          // db.SaveChanges();
            
            return View(db.Invitationforms.ToList());
        }

        // GET: InvitationForm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvitationForm invitationForm = db.Invitationforms.Find(id);
            if (invitationForm == null)
            {
                return HttpNotFound();
            }
            return View(invitationForm);
        }

        // GET: InvitationForm/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: InvitationForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,CompanyName")] InvitationForm invitationForm)
        {
            if (ModelState.IsValid)
            {
               
               // invitationForm.Token = Request.QueryString["Token"];
                invitationForm.Status = "New";
                db.Invitationforms.Add(invitationForm);
                db.SaveChanges();
              

              ViewBag.Javascript = "<script type='text/javascript' language='javascript'>alert('Invitation Sent Successfully');</script>";

             return RedirectToAction("Create");

      //     ScriptManager.RegisterStartupScript(this, this.GetType(), "krutika",Script.ToString(), true);
              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "krutika", "alert('Requirements sent successfully')", true);           
            }

            return View(invitationForm);
        }

        // GET: InvitationForm/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    InvitationForm invitationForm = db.Invitationforms.Find(id);
        //    if (invitationForm == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(invitationForm);
        //}

        //// POST: InvitationForm/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,Email,CompanyName")] InvitationForm invitationForm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       // db.Entry(invitationForm).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(invitationForm);
        //}

        // GET: InvitationForm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvitationForm invitationForm = db.Invitationforms.Find(id);
            if (invitationForm == null)
            {
                return HttpNotFound();
            }
            return View(invitationForm);
        }

        // POST: InvitationForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvitationForm invitationForm = db.Invitationforms.Find(id);
            db.Invitationforms.Remove(invitationForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //private string CreateConfirmationToken()
        //{
        //    return Guid.NewGuid();

        //}


       

        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve(InvitationForm model,string Token,string Email)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    model.Status = "Approved";
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                    //  user.ConfirmEmail = false;
                    user.EmailConfirmed = false;
                    var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                       //  _userManager.UpdateAsync(user);
                    //var result = await UserManager.ConfirmEmail(userId, code);
                    var result = await UserManager.ConfirmEmailAsync(Token, Email);
                    if (result.Succeeded)
                    {

                        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("support@promactinfo.com", "Web Registration"),
                        new System.Net.Mail.MailAddress(user.Email));
                        m.Subject = "Email confirmation";
                        m.Body = string.Format("Dear {0} <BR/>Your invitation has been approved, please click on the below link to complete your registration: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>",
                        user.UserName, Url.Action("Register", "Account",
                        new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                        m.IsBodyHtml = true;
                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("webmail.promactinfo.com");
                        smtp.Credentials = new System.Net.NetworkCredential("support@promactinfo.com", "Promact2011");
                        smtp.EnableSsl = false;
                        smtp.Send(m);

                        return RedirectToAction("ConfirmEmail", "Account", new { Email = user.Email });
                    }

                    //else
                    //{

                    //}

                }
            }
                
            //}

            catch (Exception exception)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(model);
                

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











  