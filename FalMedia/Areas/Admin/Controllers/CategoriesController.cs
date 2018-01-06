using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FalMedia.Controllers;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(_db.Categories.ToList());
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            ViewBag.Parent = new SelectList(_db.Categories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,NameAr,Description,DescriptionAr,CreatedBy")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedBy = User.Identity.Name;
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Parent = new SelectList(_db.Categories.ToList(), "Id", "Name");
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = _db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();

            ViewBag.Parent = new SelectList(_db.Categories.ToList(), "Id", "Name");
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,NameAr,Description,DescriptionAr,UpdatedDate,ModifiedBy")] Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Parent = new SelectList(_db.Categories.ToList(), "Id", "Name");
            return View(category);
        }


        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = _db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            _db.Categories.Remove(category);
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