using System.ComponentModel.DataAnnotations;

namespace FalMedia.Areas.Admin.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}