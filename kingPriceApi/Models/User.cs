using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kingPriceApi.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Sname { get; set; } = default!;

        public List<string> Groups { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }


    public class CreateUserRequest
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Sname { get; set; }

        public List<Guid> GroupIds { get; set; } = new();
    }

    public class UpdateUserRequest
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Sname { get; set; }

        public List<Guid>? GroupIds { get; set; }
    }

    public class GroupUserCountResponse
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; } = default!;
        public int UserCount { get; set; }
    }
}