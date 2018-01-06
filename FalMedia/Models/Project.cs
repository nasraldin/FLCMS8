using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FalMedia.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string TitleAr { get; set; }

        [StringLength(100)]
        public string ShortDescription { get; set; }

        [StringLength(100)]
        public string ShortDescriptionAr { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [AllowHtml]
        public string ContentAr { get; set; }

        public string Image { get; set; }
        public int ProjectTypeId { get; set; }
        public virtual ProjectType ProjectType { get; set; }
    }

    public class ProjectType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameAr { get; set; }
    }
}