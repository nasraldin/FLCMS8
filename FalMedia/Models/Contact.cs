using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalMedia.Models
{
    public class Contact : IBaseEntity
    {
        public Contact()
        {
            CreatedDate = DateTime.Now;
        }

        public string Name { get; set; }

        public string Mobile { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Company { get; set; }
        public string Subject { get; set; }

        public string Message { get; set; }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public string CreatedBy { get; set; }

        [NotMapped]
        public string ModifiedBy { get; set; }
    }
}