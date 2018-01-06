using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalMedia.Models
{
    public class Partners : IBaseEntity
    {
        public Partners()
        {
            CreatedDate = DateTime.Now;
        }

        public string Image { get; set; }

        [DefaultValue(false)]
        public bool IsFeatured { get; set; }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public DateTime? UpdatedDate { get; set; }

        public string CreatedBy { get; set; }

        [NotMapped]
        public string ModifiedBy { get; set; }
    }
}