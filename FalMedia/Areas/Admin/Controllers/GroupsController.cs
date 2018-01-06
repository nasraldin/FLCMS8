using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.Core;
using FalMedia.Areas.Admin.Models;
using FalMedia.Areas.Admin.ViewModel;
using FalMedia.Controllers;
using FalMedia.Core;
using Microsoft.AspNet.Identity.Owin;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();
        private GroupManager _groupManager;
        private RoleManager _roleManager;

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


        public ActionResult Index()
        {
            return View(GroupManager.Groups.ToList());
        }


        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var applicationgroup = await GroupManager.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (applicationgroup == null)
                return HttpNotFound();
            var groupRoles = GroupManager.GetGroupRoles(applicationgroup.Id);
            var RoleNames = groupRoles.Select(p => p.Name).ToArray();
            ViewBag.RolesList = RoleNames;
            ViewBag.PermissionsCount = RoleNames.Count();
            return View(applicationgroup);
        }


        public ActionResult Create()
        {
            ViewBag.RolesList = new SelectList(RoleManager.Roles.ToList(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Name,Description")] Group applicationgroup, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var result = await GroupManager.CreateGroupAsync(applicationgroup);
                if (result.Succeeded)
                {
                    selectedRoles = selectedRoles ?? new string[] {};
                    await GroupManager.SetGroupRolesAsync(applicationgroup.Id, selectedRoles);
                }

                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(
                RoleManager.Roles.ToList(), "Id", "Name");
            return View(applicationgroup);
        }


        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var applicationgroup = await GroupManager.FindByIdAsync(id);
            if (applicationgroup == null)
                return HttpNotFound();

            var allRoles = await RoleManager.Roles.ToListAsync();
            var groupRoles = await GroupManager.GetGroupRolesAsync(id);

            var model = new GroupViewModel
            {
                Id = applicationgroup.Id,
                Name = applicationgroup.Name,
                Description = applicationgroup.Description
            };

            foreach (var Role in allRoles)
            {
                var listItem = new SelectListItem
                {
                    Text = Role.Name,
                    Value = Role.Id,
                    Selected = groupRoles.Any(g => g.Id == Role.Id)
                };
                model.RolesList.Add(listItem);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,Name,Description")] GroupViewModel model, params string[] selectedRoles)
        {
            var group = await GroupManager.FindByIdAsync(model.Id);
            if (group == null)
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                group.Name = model.Name;
                group.Description = model.Description;
                await GroupManager.UpdateGroupAsync(group);

                selectedRoles = selectedRoles ?? new string[] {};
                await GroupManager.SetGroupRolesAsync(group.Id, selectedRoles);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var group = GroupManager.FindById(id);
            if (group == null)
                return HttpNotFound();
            GroupManager.DeleteGroup(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}