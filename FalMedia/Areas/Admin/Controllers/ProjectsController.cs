using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FalMedia.Controllers;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectsController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(_db.Projects.Include(t => t.ProjectType).ToList());
        }

        public ActionResult PTypes()
        {
            return View("Types", _db.ProjectTypes.ToList());
        }

        // GET: Admin/Categories/Create
        public ActionResult NewType()
        {
            return View("NewType");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewType([Bind(Include = "Id,Name,NameAr")] ProjectType category)
        {
            if (!ModelState.IsValid) return View(category);
            _db.ProjectTypes.Add(category);
            _db.SaveChanges();
            return RedirectToAction("PTypes");
        }

        public ActionResult NewProject()
        {
            ViewBag.ProjectTypeId = new SelectList(_db.ProjectTypes.ToList(), "Id", "Name");
            return View("NewProject");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewProject(HttpPostedFileBase thumbnail,
            [Bind(Include = "Id,Title,TitleAr,ShortDescription,ShortDescriptionAr,Content,ContentAr,Image,ProjectTypeId"
             )] Project post)
        {
            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};

            if (ModelState.IsValid)
            {
                if (thumbnail != null)
                {
                    var extension = Path.GetExtension(thumbnail.FileName);
                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("Error", "files extensions not allowed!");
                    var filename = Path.GetFileName(thumbnail.FileName);
                    var renameFile = "project-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/projects/"), renameFile);
                    thumbnail.SaveAs(path);
                    post.Image = renameFile;
                }

                _db.Projects.Add(post);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectTypeId = new SelectList(_db.ProjectTypes.ToList(), "Id", "Name");
            return View("NewProject", post);
        }

        public ActionResult EditProject(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var project = _db.Projects.Find(id);
            if (project == null)
                return HttpNotFound();
            ViewBag.ProjectTypeId = new SelectList(_db.ProjectTypes, "Id", "Name", project.ProjectTypeId);
            return View("EditProject", project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject(HttpPostedFileBase thumbnail,
            [Bind(Include = "Id,Title,TitleAr,ShortDescription,ShortDescriptionAr,Content,ContentAr,Image,ProjectTypeId"
             )] Project post)
        {
            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};

            if (ModelState.IsValid)
            {
                if (thumbnail != null)
                {
                    var extension = Path.GetExtension(thumbnail.FileName);
                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("Error", "files extensions not allowed!");
                    var filename = Path.GetFileName(thumbnail.FileName);
                    var renameFile = "project-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/projects/"), renameFile);
                    thumbnail.SaveAs(path);
                    post.Image = renameFile;
                }

                _db.Entry(post).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectTypeId = new SelectList(_db.ProjectTypes.ToList(), "Id", "Name");
            return View("EditProject", post);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult EditCat(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = _db.ProjectTypes.Find(id);
            if (category == null)
                return HttpNotFound();

            return View("EditCat", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCat(
            [Bind(Include = "Id,Name,NameAr")] ProjectType category)
        {
            if (!ModelState.IsValid) return View("EditCat", category);
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("PTypes");
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var project = _db.Projects.Find(id);
            if (project == null)
                return HttpNotFound();
            _db.Projects.Remove(project);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCat(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var projectType = _db.ProjectTypes.Find(id);
            if (projectType == null)
                return HttpNotFound();
            _db.ProjectTypes.Remove(projectType);
            _db.SaveChanges();
            return RedirectToAction("PTypes");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}