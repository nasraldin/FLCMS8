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
    public class PartnersController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // GET: Admin/Partners
        [Authorize(Roles = "Admin,View")]
        public ActionResult Index()
        {
            return View(_db.Partners.ToList());
        }

        // GET: Admin/Partners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Partners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase img,
            [Bind(Include = "Id,CreatedDate,CreatedBy,Image,IsFeatured")] Partners partners)
        {
            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    var extension = Path.GetExtension(img.FileName);
                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("Error", "files extensions not allowed!");
                    var filename = Path.GetFileName(img.FileName);
                    var renameFile = "clint-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/clints/"), renameFile);
                    img.SaveAs(path);
                    partners.Image = renameFile;
                    partners.CreatedBy = User.Identity.Name;
                    _db.Partners.Add(partners);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(partners);
        }

        // GET: Admin/Partners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var partners = _db.Partners.Find(id);
            if (partners == null)
                return HttpNotFound();
            return View(partners);
        }

        // POST: Admin/Partners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase img,
            [Bind(Include = "Id,CreatedDate,CreatedBy,Image,IsFeatured")] Partners partners)
        {
            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    var extension = Path.GetExtension(img.FileName);
                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("Error", "files extensions not allowed!");
                    var filename = Path.GetFileName(img.FileName);
                    var renameFile = "clint-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/clints/"), renameFile);
                    img.SaveAs(path);
                    partners.Image = renameFile;
                }

                partners.CreatedBy = User.Identity.Name;
                _db.Entry(partners).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(partners);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var partner = _db.Partners.Find(id);
            if (partner == null)
                return HttpNotFound();
            _db.Partners.Remove(partner);
            _db.SaveChanges();
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