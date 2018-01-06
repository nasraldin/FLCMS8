using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.Core;
using FalMedia.Areas.Admin.Models;
using FalMedia.Areas.Admin.ViewModel;
using FalMedia.Controllers;
using FalMedia.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();
        private GroupManager _groupManager;
        private RoleManager _roleManager;
        private SignInManager _signInManager;
        private UserManager _userManager;

        public AuthController()
        {
        }

        public AuthController(UserManager userManager, SignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public SignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>(); }
            private set { _signInManager = value; }
        }

        public UserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
            private set { _userManager = value; }
        }

        public GroupManager GroupManager
        {
            get { return _groupManager ?? new GroupManager(); }
            private set { _groupManager = value; }
        }

        public RoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext()
                           .Get<RoleManager>();
            }
            private set { _roleManager = value; }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        [AllowAnonymous]
        public ActionResult Login(string url)
        {
            ViewBag.ReturnUrl = url;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string url)
        {
            if (!ModelState.IsValid)
                return View(model);
            // find user by username
            var user = await UserManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var validCredentials = await UserManager.FindAsync(model.UserName, model.Password);
                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    ModelState.AddModelError("",
                        $"Your account has been locked out for {ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"]} minutes Because of several failed login attempts.");
                }
                else if (await UserManager.GetLockoutEnabledAsync(user.Id) && (validCredentials == null))
                {
                    // Record the failure which also may cause the user to be locked out
                    await UserManager.AccessFailedAsync(user.Id);
                    string message;
                    if (await UserManager.IsLockedOutAsync(user.Id))
                    {
                        message =
                            $"Your account has been locked out for {ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"]} minutes Because of several failed login attempts.";
                    }
                    else
                    {
                        var accessFailedCount = await UserManager.GetAccessFailedCountAsync(user.Id);
                        message =
                            $"Invalid login attempt. You have {accessFailedCount} more attempt(s) before your account gets locked out.";
                    }

                    ModelState.AddModelError("", message);
                }
                else if (validCredentials == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt. Please try again.");
                }
                //Add this to check if the email was confirmed.
                else if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    ModelState.AddModelError("", "You need to confirm your email address to log on.");
                    //string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");
                    //ModelState.AddModelError("", "Confirm your account-Resend.");
                    return View(model);
                }
                else
                {
                    await SignInAsync(user, model.RememberMe);
                    // When token is verified correctly, clear the access failed count used for lockout
                    await UserManager.ResetAccessFailedCountAsync(user.Id);
                    Session.Clear();
                    var currentUserId = user.Id;
                    var currentUser = _db.Users.FirstOrDefault(x => x.Id == currentUserId);
                    if (currentUser != null)
                        Session["displayName"] = currentUser.DisplayName;
                    Session["thumbnail"] = currentUser.Thumbnail;
                    Session["position"] = currentUser.Position;

                    return RedirectTo(url);
                }
            }
            else
            {
                ModelState.AddModelError("", "Not Found User Name!.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Auth");
        }

        private ActionResult RedirectTo(string url)
        {
            if (Url.IsLocalUrl(url))
                return Redirect(url);
            return RedirectToAction("Index", "Dashboard");
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent},
                await user.GenerateUserIdentityAsync(UserManager));
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
    }
}