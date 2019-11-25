using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //to validate user.
            userForRegisterDto.username = userForRegisterDto.username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.username))
                return BadRequest("User is already exist .");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.password);

            return StatusCode(201);
        }
    }
}