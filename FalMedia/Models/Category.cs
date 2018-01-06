using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FalMedia.Models
{
    public class Category : IBaseEntity
    {
        public Category()
        {
            CreatedDate = DateTime.Now;
        }

        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        [Key]
        public int Id { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}