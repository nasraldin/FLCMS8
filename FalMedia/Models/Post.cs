using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FalMedia.Models
{
    public class Post : IBaseEntity
    {
        public Post()
        {
            CreatedDate = DateTime.Now;
        }

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

        public string Thumbnail { get; set; }
        public PostStatus Status { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}