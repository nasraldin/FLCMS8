using System;
using System.Data.Entity;
using FalMedia.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FalMedia.Areas.Admin.Core
{
    public class RoleStore
        : RoleStore<Role, string, UserRole>,
            IQueryableRoleStore<Role, string>,
            IRoleStore<Role, string>, IDisposable
    {
        public RoleStore()
            : base(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        public RoleStore(DbContext context)
            : base(context)
        {
        }
    }
}