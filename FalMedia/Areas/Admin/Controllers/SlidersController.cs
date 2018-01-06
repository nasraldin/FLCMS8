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
    public class SlidersController : BaseController
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: Admin/Sliders
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        // GET: Admin/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var slider = db.Sliders.Find(id);

            if (slider == null)
                return HttpNotFound();
            ViewData["SliderName"] = slider.SliderName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSlid(int id, HttpPostedFileBase img,
            [Bind(Include = "Id,Image,Tagline,Slogan,TageDir")] Slids slids)
        {
            string[] allowedExtensions = {".jpg", ".png", ".JPG", ".PNG"};
            if (!ModelState.IsValid) return View();
            if (img == null) return RedirectToAction("Details" + "/" + id);
            var extension = Path.GetExtension(img.FileName);
            if (!allowedExtensions.Contains(extension))
                ModelState.AddModelError("Error", "files extensions not allowed!");
            var filename = Path.GetFileName(img.FileName);
            var renameFile = "slid-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
            var path = Path.Combine(Server.MapPath("~/uploads/slider/"), renameFile);
            img.SaveAs(path);
            slids.Image = renameFile;
            slids.SliderId = id;
            slids.CreatedBy = User.Identity.Name;
            db.Slids.Add(slids);
            db.SaveChanges();
            return RedirectToAction("Details" + "/" + id);
        }

        public ActionResult Slids(int id)
        {
            var slids = db.Slids.Where(s => s.SliderId.Equals(id));
            return PartialView(slids.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SliderName,CreatedDate,CreatedBy")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.CreatedBy = User.Identity.Name;
                db.Sliders.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Admin/Sliders/Edit/5
        public ActionResult EditSlid(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var slids = db.Slids.Find(id);
            if (slids == null)
                return HttpNotFound();
            ViewBag.SliderId = new SelectList(db.Sliders, "Id", "SliderName", slids.SliderId);
            return View(slids);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSlid(HttpPostedFileBase img,
            [Bind(Include = "Id,Image,Tagline,Slogan,TageDir,SliderId")] Slids slids)
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
                    var renameFile = "slid-" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + filename;
                    var path = Path.Combine(Server.MapPath("~/uploads/slider/"), renameFile);
                    img.SaveAs(path);
                    slids.Image = renameFile;
                }

                slids.CreatedBy = User.Identity.Name;
                db.Entry(slids).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details" + "/" + slids.SliderId);
            }

            ViewBag.SliderId = new SelectList(db.Sliders, "Id", "SliderName", slids.SliderId);
            return View(slids);
        }


        // GET: Admin/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var slider = db.Sliders.Find(id);
            if (slider == null)
                return HttpNotFound();
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SliderName,UpdatedDate,ModifiedBy")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.UpdatedDate = DateTime.Now;
                slider.ModifiedBy = User.Identity.Name;
                slider.CreatedBy = User.Identity.Name;
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var slider = db.Sliders.Find(id);
            if (slider == null)
                return HttpNotFound();
            db.Sliders.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteSl(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var slid = db.Slids.Find(id);
            if (slid == null)
                return HttpNotFound();
            db.Slids.Remove(slid);
            db.SaveChanges();
            return RedirectToAction("Details" + "/" + slid.SliderId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}