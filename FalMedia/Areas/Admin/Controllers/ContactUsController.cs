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
    public class ContactUsController : BaseController
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: Admin/ContactUs
        [Authorize(Roles = "Admin,View")]
        public ActionResult Index()
        {
            return View(db.Contacts.ToList());
        }

        // GET: Admin/ContactUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contact = db.Contacts.Find(id);
            if (contact == null)
                return HttpNotFound();
            return View(contact);
        }

        // GET: Admin/ContactUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ContactUs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,Name,Mobile,Email,Company,Subject,Message,CreatedDate")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Admin/ContactUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contact = db.Contacts.Find(id);
            if (contact == null)
                return HttpNotFound();
            return View(contact);
        }

        // POST: Admin/ContactUs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Name,Mobile,Email,Company,Subject,Message,CreatedDate")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Admin/ContactUs/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contact = db.Contacts.Find(id);
            if (contact == null)
                return HttpNotFound();
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}