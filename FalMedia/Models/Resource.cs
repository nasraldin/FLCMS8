using System.ComponentModel.DataAnnotations;

namespace FalMedia.Models
{
    public class Resource
    {
        [Key]
        public string Culture { get; set; }

        [Key]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}