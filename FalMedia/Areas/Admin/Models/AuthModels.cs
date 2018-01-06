using Microsoft.AspNet.Identity.EntityFramework;

namespace FalMedia.Areas.Admin.Models
{
    public class UserLogin : IdentityUserLogin<string>
    {
    }

    public class UserClaim : IdentityUserClaim<string>
    {
    }

    public class UserRole : IdentityUserRole<string>
    {
    }
}