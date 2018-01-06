using System.Collections.Generic;
using FalMedia.Models;

namespace FalMedia.Areas.Admin.ViewModel
{
    public class PostIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }

    public class AssignedCategoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}