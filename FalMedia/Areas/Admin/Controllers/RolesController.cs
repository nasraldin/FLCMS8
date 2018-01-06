using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.Models;
using FalMedia.Areas.Admin.ViewModel;
using FalMedia.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseController
    {
        private RoleManager _roleManager;
        private UserManager _userManager;

        public RolesController()
        {
        }

        public RolesController(UserManager userManager, RoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public UserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
            set { _userManager = value; }
        }

        public RoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<RoleManager>(); }
            private set { _roleManager = value; }
        }

        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var role = await RoleManager.FindByIdAsync(id);
            var users = new List<User>();
            foreach (var user in UserManager.Users.ToList())
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                    users.Add(user);

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new Role(roleViewModel.Name, roleViewModel.Description);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                    ModelState.AddModelError("", roleresult.Errors.First());
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
                return HttpNotFound();
            var roleModel = new RoleViewModel {Id = role.Id, Name = role.Name, Description = role.Description};
            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id,Description")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                role.Description = roleModel.Description;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var role = RoleManager.FindById(id);
            if (role == null)
                return HttpNotFound();

            var result = RoleManager.Delete(role);
            if (!result.Succeeded)
                ModelState.AddModelError("Error", result.Errors.First());

            return RedirectToAction("Index");
        }
    }
}