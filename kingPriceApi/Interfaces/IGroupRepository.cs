using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Entities;
using kingPriceApi.Models;

namespace kingPriceApi.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetByIdsAsync(List<Guid> ids);
        Task<List<GroupUserCountResponse>> GetUsersPerGroupAsync();
    }
}