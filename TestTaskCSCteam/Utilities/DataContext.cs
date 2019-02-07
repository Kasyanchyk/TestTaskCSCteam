using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskCSCteam.Models;

namespace TestTaskCSCteam.Utilities
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

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
            modelBuilder.Entity<User>().HasMany(x => x.Organizations).WithOne(y => y.Parent).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Organization>().HasMany(x => x.Children).WithOne(y => y.Parent).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Country>().HasMany(x => x.Children).WithOne(y => y.Parent).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Business>().HasMany(x => x.Children).WithOne(y => y.Parent).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Family>().HasMany(x => x.Children).WithOne(y => y.Parent).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Offering>().HasMany(x => x.Children).WithOne(y => y.Offering).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
