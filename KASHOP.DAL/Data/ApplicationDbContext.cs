using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<CategoryTranslation> categoryTranslations { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);


        }


    }
}
