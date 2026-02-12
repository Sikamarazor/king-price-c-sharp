using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Models;

namespace kingPriceApi.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> AddUserAsync(CreateUserRequest userDto);
        Task<UserResponse> UpdateUserAsync(UpdateUserRequest userDto, Guid id);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserResponse?> GetUserByIdAsync(Guid id);
        Task<int> GetTotalUserCountAsync();
        Task<List<GroupUserCountResponse>> GetUsersPerGroupAsync();
        Task<List<UserResponse>> GetAllUsersAsync();

    }
}