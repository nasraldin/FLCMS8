using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalMedia.Models
{
    public class Slider : IBaseEntity
    {
        public Slider()
        {
            CreatedDate = DateTime.Now;
        }

        public string SliderName { get; set; }
        public virtual ICollection<Slids> Slids { get; set; }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Slids : IBaseEntity
    {
        public Slids()
        {
            CreatedDate = DateTime.Now;
        }

        public string Image { get; set; }
        public string Tagline { get; set; }
        public string Slogan { get; set; }
        public TageDir TageDir { get; set; }

        public int SliderId { get; set; }
        public virtual Slider Slider { get; set; }

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