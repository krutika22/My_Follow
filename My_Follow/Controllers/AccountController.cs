﻿using System.Globalization;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using My_Follow.Models;
using System.Data.Entity;
using System.Web.Security;
using System.Web.Routing;

namespace IdentitySample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private ApplicationUserManager _userManager;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

      
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        ApplicationDbContext db = new ApplicationDbContext();


        [AllowAnonymous]
        public ActionResult Register(string Token, string Email)
        {

            //  string urlToken = this.Request.QueryString["Token"]

            var any = (from i in db.Invitationforms
                        where (i.Token == Token)
                        && (i.Email == Email)
                        select i).Any();
            //bool any = db.Invitationforms.Where(x => x.Token == Token && x.Email == Email).Any();
                
         //   db.SaveChanges();
            if (any == true)
            {
               // return RedirectToAction("Register", "Account");
                return View("Register");
          
            }
        

            else if (Token == null || Email == null)
            {
                return View("Error");
            }
            return View();
    
        }                                                     


                //MembershipUser user = Membership.GetUser(new Guid(token));
                //if (!user.IsApproved)
                //{
                //    user.IsApproved = true;
                //    Membership.UpdateUser(user);
                //    FormsAuthentication.SetAuthCookie(user.UserName, false);
                //    return RedirectToAction("Register");
                //}
         
            //ApplicationUser user = this.UserManager.FindByEmail(Token);
            //if (user != null)
            //{
            //    if(user.Email==Email)
            //}
            //the token came and email also
            //now you will do the whole regitration process
            // this 


            //here you will check that if this token is registerd with this email or not
            // if yes then only you will allow to open registraion form
            //else not



            // if valid then registraion form should be opened








            //if (!string.IsNullOrEmpty(Token) || !string.IsNullOrEmpty(Email))
            //    Session["Token"] = Token;
            //Session["Email"] = Email;

            //string strToken = string.Empty;
            //string strEmail = string.Empty;

            //if (Session["Token"] != null)
            //    strToken = Session["Token"].ToString();
            //if (Session["Email"] != null)
            //    strEmail = Session["Email"].ToString();

            //ViewBag.Token = strToken;
           
            //ViewBag.Email = strEmail;

           
            //string token = Request.Cookies["Token"].Value;
            //if ((urlToken == token))
            //{

            //}
            //else
            //{

            //}
           // string token = Guid.NewGuid().ToString();
            

             //string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

             //byte[] data = Convert.FromBase64String(token);
             //DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
             //if (when < DateTime.UtcNow.AddHours(-24))
             //{
             //    // too old
             //}

       //     string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
          // string token = Guid.NewGuid().ToString();  
           //string token = this.Request.QueryString["Token"];
           //  if( Request.QueryString["Token"]==(model.Token))
          //  {
               
         //   }
           //  else
           //  {

          //   }

           //Token = RouteData.Values["id"].ToString();
           

           // var routeData = Token.RouteData;
           // var stringValue = routeData.Values["id"].ToString();
           // var model = ApplicationDbContext.Equals(Token);
          
            //if (!string.IsNullOrEmpty(token))
            //{

            //}
            //MembershipUser user = Membership.GetUser(new Guid(token));

            //if (!user.IsApproved)
            //{
            //    user.IsApproved = true;
            //    Membership.UpdateUser(user);
            //    FormsAuthentication.SetAuthCookie(user.UserName, false);
            //    //return RedirectToAction("Login");
            //}

            //if (Request.Cookies["Token"].Value == Session["Token"].ToString())
            //{
      
            //}

            //else
            //{
            //    return RedirectToAction("Login");
            //}
           
       

       
   

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

           



            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    //ViewBag.Link = callbackUrl;
                    //return View("DisplayEmail");


       



                   await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                   // once he presses registraion button control will come here
                   //and only after success ful registraion here that token will be either emptied in database so that next time no one can use that token
                   return RedirectToAction("Create", "ProductOwners");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        //{
        //    ApplicationUser user = this.UserManager.FindById(Token);
        //    if (user != null)
        //    {
        //        if (user.Email == Email)
        //        {
        //            user.EmailConfirmed = true;
        //            await UserManager.UpdateAsync(user);
        //         //   await SignInAsync(user, isPersistent: false);
        //            return RedirectToAction("Index", "Home", new { ConfirmedEmail = user.Email });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Confirm", "Account", new { Email = user.Email });
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Confirm", "Account", new { Email = "" });
        //    }

        //    //if (userId == null || code == null)
        //    //{
        //    //    return View("Error");
        //    //}
        //    //var result = await UserManager.ConfirmEmailAsync(userId, code);
        //    //return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            ApplicationUser user = this.UserManager.FindById(Token);

            if (Token == null || Email == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(Token, Email);
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                UserManager.Update(user);
            }
        //    return View("ConfirmEmail");
           return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


      //  public static string generateUrlToken(string controllerName, string actionName,
      //RouteValueDictionary argumentParams, string password)
      //  {

      //      var emailConfirmationCode = await UserManager.Gene
      //  }





        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}