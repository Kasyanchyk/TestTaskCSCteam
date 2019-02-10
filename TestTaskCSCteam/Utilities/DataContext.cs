using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskCSCteam.Models;

namespace TestTaskCSCteam.Utilities
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Business> Businesses { get; set; }

        public DbSet<Family> Families { get; set; }

        public DbSet<Offering> Offerings { get; set; }

        public DbSet<Department> Departments { get; set; } 

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DataContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasMany(x => x.Countries)
                .WithOne(y => y.Parent)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Country>()
                .HasMany(x => x.Businesses)
                .WithOne(y => y.Parent)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Business>()
                .HasMany(x => x.Families)
                .WithOne(y => y.Parent)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Family>()
                .HasMany(x => x.Offeringes)
                .WithOne(y => y.Parent)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Offering>()
                .HasMany(x => x.Departments)
                .WithOne(y => y.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Country>()
                .HasIndex(t => new { t.Name, t.ParentId })
                .IsUnique();
            modelBuilder.Entity<Business>()
                .HasIndex(t => new { t.Name, t.ParentId })
                .IsUnique();
            modelBuilder.Entity<Family>()
                .HasIndex(t => new { t.Name, t.ParentId })
                .IsUnique();
            modelBuilder.Entity<Offering>()
                .HasIndex(t => new { t.Name, t.ParentId })
                .IsUnique();
            modelBuilder.Entity<Department>()
                .HasIndex(t => new { t.Name, t.ParentId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
