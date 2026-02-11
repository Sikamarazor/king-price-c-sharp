using kingPriceApi.Data;
using kingPriceApi.Entities;
using kingPriceApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace kingPriceApi.Tests.Repositories
{
    public class UserRepositoryTests
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
        public async Task AddUserAsync_ShouldAddUser()
        {
            // Arrange
            var context = await GetInMemoryDbContext();
            var repo = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Name = "John",
                Sname = "Doe"
            };

            // Act
            var result = await repo.AddUserAsync(user);
            var dbUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(dbUser);
            Assert.Equal("John", dbUser!.Name);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            var context = await GetInMemoryDbContext();
            var repo = new UserRepository(context);

            var user = new User { Id = Guid.NewGuid(), Email = "delete@test.com", Name = "Delete", Sname = "Me" };
            await repo.AddUserAsync(user);

            // Act
            var result = await repo.DeleteUserAsync(user.Id);
            var dbUser = await context.Users.FindAsync(user.Id);

            // Assert
            Assert.True(result);
            Assert.Null(dbUser);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUserWithGroups()
        {
            // Arrange
            var context = await GetInMemoryDbContext();

            var group = new Group { Id = Guid.NewGuid(), Name = "Admin" };
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "group@test.com",
                Name = "Group",
                Sname = "Test",
                Groups = { group }
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repo = new UserRepository(context);

            // Act
            var result = await repo.GetByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result!.Groups);
            Assert.Equal("Admin", result.Groups.First().Name);
        }
    }
}