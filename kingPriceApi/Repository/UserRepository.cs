using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Data;
using kingPriceApi.Entities;
using kingPriceApi.Interfaces;
using kingPriceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace kingPriceApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User userDto)
        {
            _context.Users.Add(userDto);
            await _context.SaveChangesAsync();

            return userDto;
        }

        public async Task<User?> UpdateUserAsync(User userDto, Guid id)
        {
            var searchUser = await _context.Users.FindAsync(id);

            if (searchUser == null)
            {
                return null;
            }

            searchUser.Sname = userDto.Sname;
            searchUser.Name = userDto.Name;

            await _context.SaveChangesAsync();

            return searchUser;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Groups)
                    .ThenInclude(g => g.GroupPermissions)
                        .ThenInclude(gp => gp.Permission)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<int> GetTotalUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Groups)
                    .ThenInclude(g => g.GroupPermissions)
                        .ThenInclude(gp => gp.Permission)
                .ToListAsync();
        }

    }
}