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
        private readonly IGroupRepository _groupRepository;
        public UserService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<UserResponse?> AddUserAsync(CreateUserRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return null;

            var groups = new List<Group>();

            if (request.GroupIds.Any())
            {
                groups = await _groupRepository.GetByIdsAsync(request.GroupIds);

                if (groups.Count != request.GroupIds.Count)
                    throw new Exception("One or more groups do not exist.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                Sname = request.Sname,
                Groups = groups
            };

            var createdUser = await _userRepository.AddUserAsync(user);

            return MapToResponse(createdUser);
        }

        public async Task<UserResponse?> UpdateUserAsync(UpdateUserRequest request, Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            // Email uniqueness check (if changed)
            if (!string.IsNullOrWhiteSpace(request.Email) &&
                request.Email != user.Email)
            {
                var existingUser = await _userRepository.GetByEmailAsync(request.Email);
                if (existingUser != null)
                    throw new Exception("Email already exists.");
                
                user.Email = request.Email;
            }

            // Partial updates
            if (!string.IsNullOrWhiteSpace(request.Name))
                user.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Sname))
                user.Sname = request.Sname;

            // Update Groups (if provided)
            if (request.GroupIds != null)
            {
                var groups = await _groupRepository.GetByIdsAsync(request.GroupIds);

                if (groups.Count != request.GroupIds.Count)
                    throw new Exception("One or more groups do not exist.");

                user.Groups = groups;
            }

            await _userRepository.SaveChangesAsync();

            return MapToResponse(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<UserResponse?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return MapToResponse(user);
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _userRepository.GetTotalUserCountAsync();
        }

        public async Task<List<GroupUserCountResponse>> GetUsersPerGroupAsync()
        {
            return await _groupRepository.GetUsersPerGroupAsync();
        }

        private UserResponse MapToResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Sname = user.Sname,

                Groups = user.Groups
                    .Select(g => g.Name)
                    .ToList(),

                Permissions = user.Groups
                    .SelectMany(g => g.GroupPermissions)
                    .Select(gp => gp.Permission.Name)
                    .Distinct()
                    .ToList()
            };
        }

    }
}