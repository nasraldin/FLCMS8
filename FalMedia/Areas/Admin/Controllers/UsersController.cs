using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.Core;
using FalMedia.Areas.Admin.Models;
using FalMedia.Areas.Admin.ViewModel;
using FalMedia.Controllers;
using FalMedia.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        private GroupManager _groupManager;
        private RoleManager _roleManager;
        private SignInManager _signInManager;
        private UserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(UserManager userManager, SignInManager signInManager)
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

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }

        public ActionResult AddUser()
        {
            ViewBag.GroupsList = new SelectList(GroupManager.Groups, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(HttpPostedFileBase thumbnail, AddUserViewModel userViewModel,
            params string[] selectedGroups)
        {
            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};
            if (ModelState.IsValid)
            {
                var user = new User();

                if (thumbnail != null)
                {
                    var extension = Path.GetExtension(thumbnail.FileName);
                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("Error", "files extensions not allowed!");
                    var filename = Path.GetFileName(thumbnail.FileName);
                    var renameFile = "profile-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/profile/"), renameFile);
                    thumbnail.SaveAs(path);
                    user.Thumbnail = filename;
                }

                user.UserName = userViewModel.UserName;
                user.DisplayName = userViewModel.DisplayName;
                user.AboutMe = userViewModel.AboutMe;
                user.Position = userViewModel.Position;
                user.Email = userViewModel.Email;
                user.EmailConfirmed = true;

                var adminresult = await UserManager
                    .CreateAsync(user, userViewModel.Password);

                if (adminresult.Succeeded)
                    if (selectedGroups != null)
                    {
                        selectedGroups = selectedGroups ?? new string[] {};
                        await GroupManager
                            .SetUserGroupsAsync(user.Id, selectedGroups);
                    }
                return RedirectToAction("Index");
            }

            ViewBag.GroupsList = new SelectList(GroupManager.Groups, "Id", "Name");
            ViewBag.Groups = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");
            return View();
        }


        public ActionResult Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = UserManager.FindById(id);
            var userGroups = GroupManager.GetUserGroups(id);
            ViewBag.GroupNames = userGroups.Select(u => u.Name).ToList();
            return PartialView("_Details", user);
        }

        [Authorize]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            var allGroups = GroupManager.Groups;
            var userGroups = await GroupManager.GetUserGroupsAsync(id);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Thumbnail = user.Thumbnail,
                AboutMe = user.AboutMe,
                Email = user.Email
            };

            foreach (var group in allGroups)
            {
                var listItem = new SelectListItem
                {
                    Text = group.Name,
                    Value = group.Id,
                    Selected = userGroups.Any(g => g.Id == group.Id)
                };
                model.GroupsList.Add(listItem);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HttpPostedFileBase thumbnail,
            [Bind(Include = "AboutMe,DisplayName,UserName,Email,Id")] EditUserViewModel editUser,
            params string[] selectedGroups)
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
                        if (filename != null)
                        {
                            var path = Path.Combine(Server.MapPath("~/uploads/profile/"), filename);
                            thumbnail.SaveAs(path);
                        }

                        user.UserName = editUser.UserName;
                        user.DisplayName = editUser.DisplayName;
                        user.Thumbnail = filename;
                        user.AboutMe = editUser.AboutMe;
                        user.Email = editUser.Email;
                        await UserManager.UpdateAsync(user);
                        selectedGroups = selectedGroups ?? new string[] {};
                        await GroupManager.SetUserGroupsAsync(user.Id, selectedGroups);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    user.UserName = editUser.UserName;
                    user.DisplayName = editUser.DisplayName;
                    user.AboutMe = editUser.AboutMe;
                    user.Email = editUser.Email;
                    await UserManager.UpdateAsync(user);
                    selectedGroups = selectedGroups ?? new string[] {};
                    await GroupManager.SetUserGroupsAsync(user.Id, selectedGroups);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "failed.");
            return View();
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result =
                await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                    await SignInManager.SignInAsync(user, false, false);
                TempData["success"] = "Your password has been changed";
                return RedirectToAction("Index");
            }
            AddErrors(result);
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = UserManager.FindById(id);

            if (user == null)
                return HttpNotFound();

            GroupManager.ClearUserGroups(id);
            var result = UserManager.Delete(user);
            if (!result.Succeeded)
                ModelState.AddModelError("Error", result.Errors.First());

            return RedirectToAction("Index");
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