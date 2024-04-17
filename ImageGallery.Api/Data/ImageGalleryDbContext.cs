using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Common;
using ImageGallery.Api.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Api.Data
{
    public class ImageGalleryDbContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _http;

        public ImageGalleryDbContext(DbContextOptions<ImageGalleryDbContext> options, IHttpContextAccessor http) : base(options)
        {
            _http = http;
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<Vote> Votes { get; set; }

        // Set LastEditTime and Username whether entity is added or modified. Set CreateTime when entity is added.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries()
                                        .Where(e => e.Entity is BaseModel && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                ((BaseModel)entity.Entity).LastEditTime = DateTime.Now;
                ((BaseModel)entity.Entity).Username = _http.HttpContext.User.Identity.Name;

                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreateTime = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>()
                   .HasNoKey();

            builder.Entity<IdentityUserRole<string>>()
                   .HasNoKey();

            builder.Entity<IdentityUserToken<string>>()
                   .HasNoKey();

            builder.Entity<UserImage>()
                   .HasMany(i => i.Comments)
                   .WithOne(c => c.UserImage)
                   .HasForeignKey(c => c.UserImageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserImage>()
                   .HasMany(i => i.Votes)
                   .WithOne(v => v.UserImage)
                   .HasForeignKey(v => v.UserImageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
