using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FPT_Trainning.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using FPT_Trainning.ViewModel;
using System.Data.Entity;

namespace FPT_Trainning.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
            _context = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
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

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        public async Task<ActionResult> Profile()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("TRAINER"))
            {
                ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                var trainerInfo = _context.Trainers.SingleOrDefault(t => t.TrainerId == currentUser.Id);
                if (trainerInfo == null)
                {
                    var userTrainer = new UserInfo()
                    {
                        user = currentUser,
                    };
                    return View(userTrainer);
                }
                var userInfoTrainer = new UserInfo()
                {
                    user = currentUser,
                    trainer = trainerInfo
                };
                return View(userInfoTrainer);

            }
            if (User.IsInRole("TRAINEE"))
            {
                ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                var traineeInfo = _context.Trainees.SingleOrDefault(t => t.TraineeId == currentUser.Id);

                var userInfoTrainee = new UserInfo()
                {
                    user = currentUser,
                    trainee = traineeInfo
                };
                return View(userInfoTrainee);
            }
            
            ApplicationUser user = _context.Users.FirstOrDefault(x => x.Id == userId);
            var userInfo = new UserInfo()
            {
                user = user
            };
            return View(userInfo);
        }
        [HttpGet]
        public ActionResult UpdateProfile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = _context.Users.FirstOrDefault(x => x.Id == userId);
            var userInfo = new UserInfo()
            {
                user = user
            };
            return View(userInfo);
        }
        [HttpPost]
        public ActionResult UpdateProfile(ApplicationUser user)
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            userInDb.FullName = user.FullName;
            userInDb.PhoneNumber = user.PhoneNumber;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
        [Authorize(Roles = "TRAINER,STAFF")]
        [HttpGet]
        public ActionResult UpdateTrainerProfile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
            var trainerInfo = _context.Trainers.SingleOrDefault(t => t.TrainerId == currentUser.Id);

            var userInfoTrainer = new UserInfo()
            {
                user = currentUser,
                trainer = trainerInfo
            };
            return View(userInfoTrainer);
        }
        [HttpPost]
        public ActionResult UpdateTrainerProfile(Trainer trainer)
        {
            var trainerInfoInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == trainer.TrainerId);
            trainerInfoInDb.Education = trainer.Education;
            trainerInfoInDb.Phone = trainer.Phone;
            trainerInfoInDb.WorkingPlace = trainer.WorkingPlace;
            trainerInfoInDb.Type = trainer.Type;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
        [Authorize(Roles = "STAFF,TRAINEE")]
        [HttpGet]
        public ActionResult UpdateTraineeProfile()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == userId);
            var traineeInfo = _context.Trainees.SingleOrDefault(t => t.TraineeId == currentUser.Id);

            var userInfoTrainee = new UserInfo()
            {
                user = currentUser,
                trainee = traineeInfo
            };
            return View(userInfoTrainee);
        }
        [HttpPost]
        public ActionResult UpdateTraineeProfile(Trainee trainee)
        {
            var traineeInfoInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == trainee.TraineeId);
            traineeInfoInDb.ProgramLanguage = trainee.ProgramLanguage;
            traineeInfoInDb.Age = trainee.Age;
            traineeInfoInDb.DOB = trainee.DOB;
            traineeInfoInDb.Experience = trainee.Experience;
            traineeInfoInDb.Education = trainee.Education;
            traineeInfoInDb.ToeicScore = trainee.ToeicScore;
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        public ActionResult ViewCourse()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.SingleOrDefault(x => x.Id == userId);
            if (User.IsInRole("TRAINER"))
            {
                var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == currentUser.Id);
                if (trainerInDb == null)
                {
                    TempData["MessageError"] = "You do not have Trainer Profile";
                    return RedirectToAction("Index", "Home");
                    
                }
                var courseTrainer = _context.Courses.SingleOrDefault(c => c.Id == trainerInDb.CourseId);
                if (courseTrainer == null)
                {
                    TempData["MessageError"] = "You do not have Course Assign";
                    return RedirectToAction("Index", "Home");
                }
                var courses = _context.Courses.Include(c => c.Category).ToList();

                var userInfoTrainer = new UserInfo()
                {
                    user = currentUser,
                    trainer = trainerInDb
                };
                return View(courseTrainer);

            }
            else if (User.IsInRole("TRAINEE"))
            {
                var traineeInDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == currentUser.Id);
                if (traineeInDb == null)
                {
                    TempData["MessageError"] = "You do not have Trainee Profile";
                    return RedirectToAction("Index", "Home");

                }
                var courseTrainee = _context.Courses.SingleOrDefault(c => c.Id == traineeInDb.CourseId);
                if (courseTrainee == null)
                {
                    TempData["MessageError"] = "You do not have Course Assign";
                    return RedirectToAction("Index", "Home");
                }
                var courses = _context.Courses.Include(c => c.Category).ToList();

                var userInfoTrainee = new UserInfo()
                {
                    user = currentUser,
                    trainee = traineeInDb
                };
                return View(courseTrainee);
            }
            return HttpNotFound();
        }
        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
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

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
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

        //
        // GET: /Account/Register
        [Authorize(Roles="ADMIN,STAFF")]
        public ActionResult Register()
        {
            if (User.IsInRole("ADMIN"))
                ViewBag.Name = _context.Roles.Where(r => r.Name.Equals("STAFF") || r.Name.Equals("TRAINER"));
            if (User.IsInRole("STAFF"))
                ViewBag.Name = _context.Roles.Where(r => r.Name.Equals("TRAINEE")).ToList();
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles ="ADMIN,STAFF")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkuser = _context.Users.Where(u => u.UserName == model.Email);
                if (checkuser.Any())
                {
                    TempData["MessageError"] = "Can not Register, UserName already Existed";
                    return RedirectToAction("Index", "Home");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                var userStore = new UserStore<ApplicationUser>(_context);
                var userManager = new UserManager<ApplicationUser>(userStore);


                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, model.RoleName);
                    if (model.RoleName.Equals("TRAINER"))
                    {

                        var newTrainer = new Trainer()
                        {
                            TrainerId = user.Id,
                           
                        };
                        _context.Trainers.Add(newTrainer);
                        _context.SaveChanges();
                        TempData["MessageSuccess"] = "Register Trainer Successfully";
                        return RedirectToAction("Index", "Trainers");
                    }
                    if (model.RoleName.Equals("TRAINEE"))
                    {
                        var newTrainee = new Trainee()
                        {
                            TraineeId = user.Id,
                            
                        };
                        _context.Trainees.Add(newTrainee);
                        _context.SaveChanges();
                        TempData["MessageSuccess"] = "Register Trainee Successfully";
                        return RedirectToAction("Index", "Trainees");
                    }
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Staffs");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
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

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
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
        public ActionResult ResetPassword(string id)
        {
            if (User.IsInRole("ADMIN") || User.IsInRole("STAFF"))
            {
                var user1 = _context.Users.SingleOrDefault(u => u.Id == id);
                if (user1 == null) return HttpNotFound();
                var resetUser1 = new ResetPasswordViewModel()
                {
                    User = user1
                };
                return View(resetUser1);
            }
            var currentUserId = User.Identity.GetUserId();
            ApplicationUser webUser = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            var user2 = _context.Users.SingleOrDefault(u => u.Id == webUser.Id);
            if (user2 == null) return HttpNotFound();
            var resetUser2 = new ResetPasswordViewModel()
            {
                User = webUser
            };
            return View(resetUser2);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.User.UserName);
            if (user == null)
            {
               
                return RedirectToAction("Index", "Home");
            }
            await UserManager.RemovePasswordAsync(user.Id);
            var result = await UserManager.AddPasswordAsync(user.Id, model.Password);
            if (result.Succeeded)
            {
                TempData["MessageSuccess"] = "You Resest Password Successfully";
                return RedirectToAction("Index", "Home");
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
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
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
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
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
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
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

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Account");
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

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