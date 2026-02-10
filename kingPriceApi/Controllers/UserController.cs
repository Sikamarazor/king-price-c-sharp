using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using kingPriceApi.Helpers;
using kingPriceApi.Interfaces;
using kingPriceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace kingPriceApi.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRequest request)
        {
            var result = await _userService.AddUserAsync(request);

            if (result == null)
                return BadRequest(ApiResponse<UserResponse>.Error("User already exists or could not be created"));

            return Ok(ApiResponse<UserResponse>.SuccessMessage(result, "User created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request)
        {
            var result = await _userService.UpdateUserAsync(request, id);

            if (result == null)
            {
                return NotFound(
                    ApiResponse<UserResponse>.Error($"No user found with ID {id}")
                );
            }

            return Ok(
                ApiResponse<UserResponse>.SuccessMessage(result, "User updated successfully")
            );
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            if (!deleted)
                return NotFound($"No user found with ID {id}");

            return Ok( "User deleted successfully");
        }

    }
}