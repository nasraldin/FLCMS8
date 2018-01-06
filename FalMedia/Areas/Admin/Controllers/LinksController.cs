using System.Linq;
using System.Net;
using System.Web.Mvc;
using FalMedia.Controllers;
using FalMedia.Core;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LinksController : BaseController
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: Admin/Links
        [Authorize(Roles = "Admin,View")]
        public ActionResult Index()
        {
            return View(db.Links.ToList());
        }

        // GET: Admin/Links/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Links/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LinkId,LinkName,LinkUrl,LinkTarget,LinkDescription")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(link);
        }

        // GET: Admin/Links/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var link = db.Links.Find(id);
            if (link == null)
                return HttpNotFound();
            return View(link);
        }

        // POST: Admin/Links/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LinkId,LinkUrl,LinkTarget,LinkDescription")] Link link)
        {
            var updateLink = db.Set<Link>().FirstOrDefault(x => x.LinkId == link.LinkId);
            if (ModelState.IsValid && (updateLink != null))
            {
                updateLink.LinkUrl = link.LinkUrl;
                updateLink.LinkTarget = link.LinkTarget;
                updateLink.LinkDescription = link.LinkDescription;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(link);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}