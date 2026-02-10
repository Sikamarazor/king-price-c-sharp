using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Entities;

namespace kingPriceApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User userDto);
        Task<User?> UpdateUserAsync(User userDto, int id);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
    }
}