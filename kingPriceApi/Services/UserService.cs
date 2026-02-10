using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Entities;
using kingPriceApi.Interfaces;
using kingPriceApi.Models;

namespace kingPriceApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> AddUserAsync(UserRequest userDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);

            if (existingUser != null)
            {
                return null;
            }

            var newUser = new User
            {
                Name = userDto.Name,
                Sname = userDto.Sname,
                Email = userDto.Email
            };

            var user = await _userRepository.AddUserAsync(newUser);

            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Sname = user.Sname,
                Email = user.Email
            };
        }

        public async Task<UserResponse> UpdateUserAsync(UserRequest userDto, int id)
        {
            var updateUser = new User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Sname = userDto.Sname
            };

            var user = await _userRepository.UpdateUserAsync(updateUser, id);

            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Sname = user.Sname
            };
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

    }
}