using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FalMedia.Areas.Admin.Models
{
    public class Role : IdentityRole<string, UserRole>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Role(string name, string description)
            : this()
        {
            Name = name;
            Description = description;
        }

        public virtual string Description { get; set; }
    }
}