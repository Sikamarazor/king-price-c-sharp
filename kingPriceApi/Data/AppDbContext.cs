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
        public DbSet<GroupPermission> GroupPermissions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity(j =>
                {
                    j.ToTable("user_groups");
                });

            // Group <-> Permission (many-to-many explicit)
            modelBuilder.Entity<GroupPermission>()
                .HasKey(gp => new { gp.GroupsId, gp.PermissionsId });

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Group)
                .WithMany(g => g.GroupPermissions)
                .HasForeignKey(gp => gp.GroupsId);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Permission)
                .WithMany(p => p.GroupPermissions)
                .HasForeignKey(gp => gp.PermissionsId);

            modelBuilder.Entity<GroupPermission>()
                .ToTable("group_permissions");


            var adminGroupId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userGroupId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var createId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var viewId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = adminGroupId, Name = "Admin" },
                new Group { Id = userGroupId, Name = "User" }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = createId, Name = "Create" },
                new Permission { Id = viewId, Name = "View" }
            );

            modelBuilder.Entity<GroupPermission>().HasData(
                new GroupPermission
                {
                    GroupsId = adminGroupId,
                    PermissionsId = createId
                },
                new GroupPermission
                {
                    GroupsId = adminGroupId,
                    PermissionsId = viewId
                },
                new GroupPermission
                {
                    GroupsId = userGroupId,
                    PermissionsId = viewId
                }
            );

        }
    }
}