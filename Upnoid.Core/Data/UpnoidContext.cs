using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Upnoid.Domain.Models;
using System.Linq;
using Upnoid.Domain.Abstracts;
using System;
using Microsoft.AspNetCore.Identity;

namespace Upnoid.Core.Data
{
    public class UpnoidContext : IdentityDbContext<ApplicationUser>
    {
        public UpnoidContext(DbContextOptions<UpnoidContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Trailer> Trailer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                
            //builder.Entity<ApplicationUser>()
            //    .HasMany(e => e.Claims)
            //    .WithOne()
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //model level filters
            builder.Entity<Customer>()
                .HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Movie>()
                .HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Rental>()
                .HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Trailer>()
                .HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Genre>()
                .HasQueryFilter(p => !p.IsDeleted);
        }

        

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseModel)entry.Entity).DateAdded = DateTime.UtcNow;
                    ((BaseModel)entry.Entity).IsDeleted = false;
                }
                if(entry.State == EntityState.Modified)
                {
                    ((BaseModel)entry.Entity).ModifiedDate = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Deleted)
                {
                    ((BaseModel)entry.Entity).ModifiedDate = DateTime.UtcNow;
                    ((BaseModel)entry.Entity).IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
