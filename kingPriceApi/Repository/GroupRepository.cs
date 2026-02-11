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
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetByIdsAsync(List<Guid> ids)
        {
            return await _context.Groups
                .Where(g => ids.Contains(g.Id))
                .Include(g => g.GroupPermissions)
                    .ThenInclude(gp => gp.Permission)
                .ToListAsync();
        }

        public async Task<List<GroupUserCountResponse>> GetUsersPerGroupAsync()
        {
            return await _context.Groups
                .Select(g => new GroupUserCountResponse
                {
                    GroupId = g.Id,
                    GroupName = g.Name,
                    UserCount = g.Users.Count
                })
                .ToListAsync();
        }
    }
}