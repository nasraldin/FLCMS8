using System.Data.Entity;
using FalMedia.Areas.Admin.Models;
using FalMedia.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FalMedia.Core
{
    public class AppDbContext : IdentityDbContext<User, Role, string, UserLogin, UserRole, UserClaim>
    {
        public AppDbContext()
            : base("FalConnection")
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Slids> Slids { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithRequired().HasForeignKey(ag => ag.GroupId);
            modelBuilder.Entity<UserGroup>()
                .HasKey(r =>
                    new
                    {
                        r.UserId,
                        r.GroupId
                    }).ToTable("UserGroups");

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Roles)
                .WithRequired().HasForeignKey(ap => ap.GroupId);
            modelBuilder.Entity<GroupRole>().HasKey(gr =>
                new
                {
                    gr.RoleId,
                    gr.GroupId
                }).ToTable("GroupRoles");

            modelBuilder.Entity<Group>().ToTable("Groups");
            modelBuilder.Entity<User>().ToTable("Users").
                Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims").
                Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<Role>().ToTable("Roles").
                Property(p => p.Id).HasColumnName("RoleId");

            modelBuilder.Entity<Resource>().HasKey(r => new {r.Name, r.Culture});

            modelBuilder.Entity<Category>()
                .HasMany(s => s.Posts)
                .WithMany(c => c.Categories)
                .Map(cs =>
                {
                    cs.MapLeftKey("CategoryId");
                    cs.MapRightKey("PostId");
                    cs.ToTable("CategoryPost");
                });

            modelBuilder.Entity<Slider>()
                .HasMany(g => g.Slids)
                .WithRequired().HasForeignKey(ag => ag.SliderId);
        }
    }
}