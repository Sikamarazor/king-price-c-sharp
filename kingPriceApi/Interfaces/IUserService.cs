using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Models;

namespace kingPriceApi.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> AddUserAsync(UserRequest userDto);
        Task<UserResponse> UpdateUserAsync(UserRequest userDto, int id);
        Task<bool> DeleteUserAsync(Guid id);
    }
}