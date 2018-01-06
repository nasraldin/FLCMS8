using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FalMedia.Areas.Admin.Models;
using FalMedia.Controllers;
using FalMedia.Core;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // GET: Admin/Settings
        public ActionResult Index()
        {
            var settings = _db.Settings.Find(1);
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSettings(HttpPostedFileBase logo,
            [Bind(
                 Include =
                     "Id,SiteTitle,Tagline,SiteLogo,EmailAddress,SiteLanguage,Address,Mobile,PhoneNumber,GoogleMap,MetaTage"
             )] Settings settings)
        {
            string[] allowedExtensions = {".jpg", ".png", ".svg", ".JPG", ".PNG", ".SVG"};
            if (!ModelState.IsValid) return View("Index", settings);
            if (logo != null)
            {
                var extension = Path.GetExtension(logo.FileName);
                if (!allowedExtensions.Contains(extension))
                    ModelState.AddModelError("Error", "files extensions not allowed!");
                var filename = Path.GetFileName(logo.FileName);
                if (filename != null)
                {
                    var path = Path.Combine(Server.MapPath("~/uploads/logo/"), filename);
                    logo.SaveAs(path);
                    settings.SiteLogo = filename;
                }
            }

            _db.Entry(settings).State = EntityState.Modified;
            _db.SaveChanges();
            TempData["success"] = "Update successfully Settings";
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