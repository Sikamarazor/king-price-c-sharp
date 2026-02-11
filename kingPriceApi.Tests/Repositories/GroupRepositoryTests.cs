using kingPriceApi.Data;
using kingPriceApi.Entities;
using kingPriceApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace kingPriceApi.Tests.Repositories
{
    public class GroupRepositoryTests
    {
        private async Task<AppDbContext> GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task GetByIdsAsync_ShouldReturnGroupsWithPermissions()
        {
            // Arrange
            var context = await GetInMemoryDbContext();

            var permission = new Permission { Id = Guid.NewGuid(), Name = "Create" };
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                GroupPermissions = new List<GroupPermission>
                {
                    new GroupPermission { Permission = permission }
                }
            };

            context.Groups.Add(group);
            await context.SaveChangesAsync();

            var repo = new GroupRepository(context);

            // Act
            var result = await repo.GetByIdsAsync(new List<Guid> { group.Id });

            // Assert
            Assert.Single(result);
            Assert.Single(result[0].GroupPermissions);
            Assert.Equal("Create", result[0].GroupPermissions.First().Permission.Name);
        }

        [Fact]
        public async Task GetUsersPerGroupAsync_ShouldReturnCorrectCounts()
        {
            // Arrange
            var context = await GetInMemoryDbContext();

            var group = new Group { Id = Guid.NewGuid(), Name = "View" };
            var user1 = new User { Id = Guid.NewGuid(), Email = "a@test.com", Name = "User1", Sname = "One", Groups = { group } };
            var user2 = new User { Id = Guid.NewGuid(), Email = "b@test.com", Name = "User2", Sname = "Two", Groups = { group } };

            context.Users.AddRange(user1, user2);
            context.Groups.Add(group);
            await context.SaveChangesAsync();

            var repo = new GroupRepository(context);

            // Act
            var result = await repo.GetUsersPerGroupAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("View", result[0].GroupName);
            Assert.Equal(2, result[0].UserCount);
        }
    }
}