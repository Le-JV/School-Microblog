using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microblog.Models;

namespace Microblog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserInterests>()
                .HasKey(t => new { t.ApplicationUserId, t.InterestId });

            builder.Entity<UserInterests>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserInterests)
                .HasForeignKey(pt => pt.ApplicationUserId);

            builder.Entity<UserInterests>()
                .HasOne(pt => pt.Interest)
                .WithMany(t => t.UserInterests)
                .HasForeignKey(pt => pt.InterestId);

            builder.Entity<PostInterests>()
                .HasKey(t => new { t.PostId, t.InterestId });

            builder.Entity<PostInterests>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostInterests)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<PostInterests>()
                .HasOne(pt => pt.Interest)
                .WithMany(t => t.PostInterests)
                .HasForeignKey(pt => pt.InterestId);
        }

        public DbSet<Post> Post { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Interest> Interest { get; set; }

        public DbSet<UserInterests> UserInterests { get; set; }

        public DbSet<PostInterests> PostInterests { get; set; }
    }

    public class UserInterests
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }

    public class PostInterests
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
