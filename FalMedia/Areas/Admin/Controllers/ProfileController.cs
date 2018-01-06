using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.ViewModel;
using FalMedia.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfileController : BaseController
    {
        private UserManager _userManager;

        public ProfileController()
        {
        }

        public ProfileController(UserManager userManager)
        {
            UserManager = userManager;
        }


        public UserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
            private set { _userManager = value; }
        }

        // GET: Admin/Profile
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProfileUpdate(HttpPostedFileBase thumbnail,
            [Bind(Include = "AboutMe,DisplayName,UserName,Email,Id,Position")] EditUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                    return HttpNotFound();

                string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};
                if (thumbnail != null)
                {
                    var extension = Path.GetExtension(thumbnail.FileName);
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("CustomError", "files extensions not allowed!");
                    }
                    else
                    {
                        var filename = Path.GetFileName(thumbnail.FileName);
                        var renameFile = "profile-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute +
                                         filename;
                        var path = Path.Combine(Server.MapPath("~/uploads/profile/"), renameFile);
                        thumbnail.SaveAs(path);
                        user.UserName = editUser.UserName;
                        user.DisplayName = editUser.DisplayName;
                        user.Thumbnail = renameFile;
                        user.AboutMe = editUser.AboutMe;
                        user.Email = editUser.Email;
                        user.Position = editUser.Position;
                        await UserManager.UpdateAsync(user);
                        TempData["success"] = "Your Profile Update successfully";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    user.UserName = editUser.UserName;
                    user.DisplayName = editUser.DisplayName;
                    user.AboutMe = editUser.AboutMe;
                    user.Email = editUser.Email;
                    user.Position = editUser.Position;
                    await UserManager.UpdateAsync(user);
                    TempData["success"] = "Your Profile Update successfully";
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "failed.");
            return View();
        }
    }
}