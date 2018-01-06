using System.ComponentModel.DataAnnotations;

namespace FalMedia.Models
{
    public class Link
    {
        [Key]
        public int LinkId { set; get; }

        public string LinkName { get; set; }

        [Url]
        public string LinkUrl { get; set; }

        public LinkTarget LinkTarget { get; set; }
        public string LinkDescription { get; set; }
    }
}