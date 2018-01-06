using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FalMedia.Areas.Admin.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            RolesList = new List<SelectListItem>();
            GroupsList = new List<SelectListItem>();
        }

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "AboutMe")]
        public string AboutMe { get; set; }

        [Display(Name = "Thumbnail")]
        public string Thumbnail { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        public ICollection<SelectListItem> RolesList { get; set; }
        public ICollection<SelectListItem> GroupsList { get; set; }
    }
}