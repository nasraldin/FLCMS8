using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FalMedia.Areas.Admin.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }

        public string SiteTitle { get; set; }
        public string Tagline { get; set; }
        public string SiteLogo { get; set; }
        public string EmailAddress { get; set; }
        public string SiteLanguage { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public string GoogleMap { get; set; }

        [AllowHtml]
        public string MetaTage { get; set; }
    }
}