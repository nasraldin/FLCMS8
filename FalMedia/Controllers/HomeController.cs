using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FalMedia.Core;
using FalMedia.Core.Culture;
using FalMedia.Models;

namespace FalMedia.Controllers
{
    [SetCulture]
    public class HomeController : BaseController
    {
        private readonly AppDbContext _db = new AppDbContext();

        public ActionResult Index()
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

            ViewBag.slider = GetSlider("home");
            ViewBag.sliderAr = GetSlider("homeAr");
            ViewBag.Aboutus = GetAboutUs();
            ViewBag.Partners = GetOurPartners();
            ViewBag.Services = GetOurServices();
            ViewBag.Projects = GetProjects();
            ViewBag.LatestNews = GetLatestNews();

            GetSettings();
            GetHomeContent();
            return View();
        }

        private void GetSettings()
        {
            var setting = _db.Settings.FirstOrDefault(s => s.Id.Equals(1));

            if (setting == null) return;
            ViewBag.SiteTitle = setting.SiteTitle;
            ViewBag.Tagline = setting.Tagline;
            ViewBag.SiteLogo = setting.SiteLogo;
            ViewBag.EmailAddress = setting.EmailAddress;
            ViewBag.Address = setting.Address;
            ViewBag.PhoneNumber = setting.PhoneNumber;
            ViewBag.Mobile = setting.Mobile;
            ViewBag.SiteLanguage = setting.SiteLanguage;
            ViewBag.GoogleMap = setting.GoogleMap;
            ViewBag.MetaTage = setting.MetaTage;
        }

        private IEnumerable GetOurPartners()
        {
            var partners = _db.Partners.Where(p => p.IsFeatured.Equals(true));

            //var allPartners = _db.Posts.Where(x => x.Categories.Any(r => r.Id.Equals(partners.Id)));

            return partners;
        }

        private IEnumerable GetAboutUs()
        {
            var aboutus = _db.Posts.Where(x => x.Categories.Any(r => r.Id.Equals(1)));
            return aboutus;
        }

        private IEnumerable GetOurServices()
        {
            var allServices = _db.Posts.Where(x => x.Categories.Any(r => r.Id.Equals(2)));
            return allServices;
        }

        private IEnumerable GetLatestNews()
        {
            var latestNews = _db.Posts.Where(x => x.Categories.Any(r => r.Id.Equals(6)));
            return latestNews;
        }

        private IEnumerable GetProjects()
        {
            var allprojects = _db.Projects.Include(t => t.ProjectType);
            return allprojects;
        }

        private void GetHomeContent()
        {
            var aboutTitle = _db.Categories.FirstOrDefault(s => s.Id.Equals(1));
            if (aboutTitle != null)
            {
                ViewData["aboutTitle"] = aboutTitle.Name;
                ViewData["aboutTitleAr"] = aboutTitle.NameAr;
                ViewData["aboutDescription"] = aboutTitle.Description;
                ViewData["aboutDescriptionAr"] = aboutTitle.DescriptionAr;
            }


            var servicesTitle = _db.Categories.FirstOrDefault(s => s.Id.Equals(2));
            if (servicesTitle != null)
            {
                ViewData["servTitle"] = servicesTitle.Name;
                ViewData["servTitleAr"] = servicesTitle.NameAr;
                ViewData["servicesDescription"] = servicesTitle.Description;
                ViewData["servicesDescriptionAr"] = servicesTitle.DescriptionAr;
            }

            var projectsTitle = _db.Categories.FirstOrDefault(s => s.Id.Equals(3));
            if (projectsTitle != null)
            {
                ViewData["projectsTitle"] = projectsTitle.Name;
                ViewData["projectsTitleAr"] = projectsTitle.NameAr;
                ViewData["projectsDescription"] = projectsTitle.Description;
                ViewData["projectsDescriptionAr"] = projectsTitle.DescriptionAr;
            }

            var partnersTitle = _db.Categories.FirstOrDefault(s => s.Id.Equals(4));
            if (partnersTitle != null)
            {
                ViewData["partnersTitle"] = partnersTitle.Name;
                ViewData["partnersTitleAr"] = partnersTitle.NameAr;
                ViewData["partnersDescription"] = partnersTitle.Description;
                ViewData["partnersDescriptionAr"] = partnersTitle.DescriptionAr;
            }

            var contactTitle = _db.Categories.FirstOrDefault(s => s.Id.Equals(5));
            if (contactTitle != null)
            {
                ViewData["contactTitle"] = contactTitle.Name;
                ViewData["contactTitleAr"] = contactTitle.NameAr;
                ViewData["contactDescription"] = contactTitle.Description;
                ViewData["contactDescriptionAr"] = contactTitle.DescriptionAr;
            }
        }

        private IEnumerable GetSlider(string name)
        {
            var slids = _db.Slids.Where(s => s.Slider.SliderName.Equals(name)).ToList();
            return slids;
        }

        private Link GetLinks(string linkName)
        {
            return _db.Links.FirstOrDefault(f => f.LinkName.Equals(linkName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "Id,Name,Mobile,Email,Company,Subject,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _db.Contacts.Add(contact);
                _db.SaveChanges();
            }

            ViewBag.Message = "Information has been submitted.";

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
            return View();
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