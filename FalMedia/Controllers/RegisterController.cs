using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.Models;
using FalMedia.Areas.Admin.ViewModel;
using Microsoft.AspNet.Identity.Owin;

namespace FalMedia.Controllers
{
    public class RegisterController : BaseController
    {
        private UserManager _userManager;

        public RegisterController()
        {
        }

        public RegisterController(UserManager userManager)
        {
            UserManager = userManager;
        }

        public UserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
            private set { _userManager = value; }
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return PartialView("Register");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(AddUserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return PartialView("Register");
            var user = new User
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email
            };

            await UserManager.CreateAsync(user, userViewModel.Password);

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

            base.Dispose(disposing);
        }
    }
}