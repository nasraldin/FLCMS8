using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FalMedia.Areas.Admin.Models
{
    public class User : IdentityUser<string, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string DisplayName { get; set; }
        public string AboutMe { get; set; }
        public string Thumbnail { get; set; }
        public string Position { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, string> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}