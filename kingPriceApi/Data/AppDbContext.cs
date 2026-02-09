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

            // 🔹 User <-> Group (many-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity(j => j.ToTable("UserGroups"));

            // 🔹 Group <-> Permission (many-to-many)
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Permissions)
                .WithMany(p => p.Groups)
                .UsingEntity(j => j.ToTable("GroupPermissions"));

            var adminGroupId = Guid.NewGuid();
            var userGroupId = Guid.NewGuid();

            var readPermissionId = Guid.NewGuid();
            var writePermissionId = Guid.NewGuid();

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = adminGroupId, Name = "Admin" },
                new Group { Id = userGroupId, Name = "User" }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = readPermissionId, Name = "Read" },
                new Permission { Id = writePermissionId, Name = "Write" }
            );

            modelBuilder.Entity("GroupPermissions").HasData(
                new { GroupsId = adminGroupId, PermissionsId = readPermissionId },
                new { GroupsId = adminGroupId, PermissionsId = writePermissionId },
                new { GroupsId = userGroupId, PermissionsId = readPermissionId }
            );

        }
    }
}