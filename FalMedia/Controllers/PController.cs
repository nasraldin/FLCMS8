using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FalMedia.Core;
using FalMedia.Core.Culture;
using FalMedia.Models;

namespace FalMedia.Controllers
{
    [SetCulture]
    public class PController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();

        // GET: P
        public ActionResult V(int? id)
        {
            #region socialMedia

            var fb = GetLinks("facebook");
            if (fb != null)
            {
                ViewBag.facebook = fb.LinkUrl;
                ViewBag.facebookt = fb.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }

            var tw = GetLinks("twitter");
            if (tw != null)
            {
                ViewBag.twitter = tw.LinkUrl;
                ViewBag.twittert = tw.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }

            var inst = GetLinks("instagram");
            if (inst != null)
            {
                ViewBag.instagram = inst.LinkUrl;
                ViewBag.instagramt = inst.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }


            var ytu = GetLinks("youtube");
            if (ytu != null)
            {
                ViewBag.youtube = ytu.LinkUrl;
                ViewBag.youtubet = ytu.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }


            var lnk = GetLinks("linkedin");
            if (lnk != null)
            {
                ViewBag.linkedin = lnk.LinkUrl;
                ViewBag.linkedint = lnk.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }

            #endregion

            GetSettings();
            ViewBag.LatestNews = GetLatestNews();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var post = _db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();
            ViewData["PosTitle"] = post.Title;
            ViewData["PosTitleAr"] = post.TitleAr;
            ViewData["PosTContent"] = post.Content;
            ViewData["PosTContentAr"] = post.ContentAr;
            ViewData["PosThumbnail"] = post.Thumbnail;
            ViewData["PosTDescription"] = post.ShortDescription;
            ViewData["PosTDescriptionAr"] = post.ShortDescriptionAr;
            return View();
        }

        // GET: P
        public ActionResult P(int? id)
        {
            #region socialMedia

            var fb = GetLinks("facebook");
            if (fb != null)
            {
                ViewBag.facebook = fb.LinkUrl;
                ViewBag.facebookt = fb.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }

            var tw = GetLinks("twitter");
            if (tw != null)
            {
                ViewBag.twitter = tw.LinkUrl;
                ViewBag.twittert = tw.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }

            var inst = GetLinks("instagram");
            if (inst != null)
            {
                ViewBag.instagram = inst.LinkUrl;
                ViewBag.instagramt = inst.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }


            var ytu = GetLinks("youtube");
            if (ytu != null)
            {
                ViewBag.youtube = ytu.LinkUrl;
                ViewBag.youtubet = ytu.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }


            var lnk = GetLinks("linkedin");
            if (lnk != null)
            {
                ViewBag.linkedin = lnk.LinkUrl;
                ViewBag.linkedint = lnk.LinkTarget;
            }
            else
            {
                ViewBag.facebook = "#";
                ViewBag.facebookt = LinkTarget._blank;
            }

            #endregion

            GetSettings();
            ViewBag.LatestNews = GetLatestNews();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var post = _db.Projects.Find(id);
            if (post == null)
                return HttpNotFound();
            ViewData["PosTitle"] = post.Title;
            ViewData["PosTitleAr"] = post.TitleAr;
            ViewData["PosTContent"] = post.Content;
            ViewData["PosTContentAr"] = post.ContentAr;
            ViewData["PosTImage"] = post.Image;
            ViewData["PosTDescription"] = post.ShortDescription;
            ViewData["PosTDescriptionAr"] = post.ShortDescriptionAr;
            return View();
        }

        private void GetSettings()
        {
            var setting = _db.Settings.FirstOrDefault(s => s.Id.Equals(1));

            if (setting != null)
            {
                ViewBag.SiteTitle = setting.SiteTitle;
                ViewBag.Tagline = setting.Tagline;
                ViewBag.SiteLogo = setting.SiteLogo;
                ViewBag.EmailAddress = setting.EmailAddress;
                ViewBag.Address = setting.Address;
                ViewBag.PhoneNumber = setting.PhoneNumber;
                ViewBag.SiteLanguage = setting.SiteLanguage;
                ViewBag.GoogleMap = setting.GoogleMap;
            }
        }

        private IEnumerable GetLatestNews()
        {
            var latestNews = _db.Posts.Where(x => x.Categories.Any(r => r.Id.Equals(6)));
            return latestNews;
        }

        private Link GetLinks(string linkName)
        {
            return _db.Links.FirstOrDefault(f => f.LinkName.Equals(linkName));
        }
    }
}