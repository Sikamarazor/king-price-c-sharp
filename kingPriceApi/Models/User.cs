using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kingPriceApi.Models
{
    public class UserRequest
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Sname { get; set; }
    }
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Sname { get; set; } = string.Empty;
    }
}