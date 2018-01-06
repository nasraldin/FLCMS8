using System;
using System.Data.Entity;
using FalMedia.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FalMedia.Areas.Admin.Core
{
    public class UserStore
        : UserStore<User, Role, string,
                UserLogin, UserRole,
                UserClaim>, IUserStore<User, string>,
            IDisposable
    {
        public UserStore()
            : this(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        public UserStore(DbContext context)
            : base(context)
        {
        }
    }
}