using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FalMedia.Controllers;
using FalMedia.Core;

namespace FalMedia.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            ViewBag.Allusers = AllUsers();
            return View();
        }

        private int AllUsers()
        {
            return _db.Users.Count();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;
            // Save culture in a cookie
            var cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture; // update cookie value
            else
                cookie = new HttpCookie("_culture")
                {
                    Value = culture,
                    Expires = DateTime.Now.AddYears(1)
                };

            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
    }
}