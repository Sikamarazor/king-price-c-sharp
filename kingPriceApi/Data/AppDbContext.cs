using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace kingPriceApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Group> Groups { get; set; } = null!;

        public DbSet<Permission> Permissions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User <-> Group (many-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity(j => j.ToTable("user_groups"));

            // Group <-> Permission (many-to-many)
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Permissions)
                .WithMany(p => p.Groups)
                .UsingEntity(j => j.ToTable("group_permissions"));
        }
    }
}