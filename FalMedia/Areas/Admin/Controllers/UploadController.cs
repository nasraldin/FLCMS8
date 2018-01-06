using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FalMedia.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        // GET: Admin/Upload

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FileUpload(HttpPostedFileBase file)
        {
            var location = SaveFile(Server.MapPath("~/uploads/"), file);
            return Json(new {location}, JsonRequestBehavior.AllowGet);
        }

        private static string SaveFile(string targetFolder, HttpPostedFileBase file)
        {
            const int megabyte = 1024*1024;

            if (!file.ContentType.StartsWith("image/"))
                throw new InvalidOperationException("Invalid MIME content type.");

            var extension = Path.GetExtension(file.FileName.ToLowerInvariant());
            string[] extensions = {".gif", ".jpg", ".png", ".JPG", ".PNG", ".svg"};
            if (!extensions.Contains(extension))
                throw new InvalidOperationException("Invalid file extension.");

            if (file.ContentLength > 60*megabyte)
                throw new InvalidOperationException("File size limit exceeded.");

            var fileName = Guid.NewGuid() + extension;
            var path = Path.Combine(targetFolder, fileName);
            file.SaveAs(path);
            return fileName;
            //return Path.Combine("/uploads", fileName).Replace('\\', '/');
        }
    }
}