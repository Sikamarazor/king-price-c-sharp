using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Data;
using kingPriceApi.Entities;
using kingPriceApi.Interfaces;
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

        public async Task<User?> UpdateUserAsync(User userDto, int id)
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
    }
}