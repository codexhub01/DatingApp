using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context , ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if(await UserExist(register.UserName))
            {
                return BadRequest("Username is already taken");
            }
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = register.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var data = await _context.Users.SingleOrDefaultAsync(s=>s.UserName == loginDto.UserName);
            
            if(data == null)
            {
                return Unauthorized("Invalid UserName");
            }
            
            using var hmac = new HMACSHA512(data.PasswordSalt);

            var computehash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //This we are doing because its in bytes
            for(int i = 0 ; i < computehash.Length ; i++)
            {
                if(computehash[i] != data.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }

            return new UserDto
            {
                UserName = data.UserName,
                Token = _tokenService.CreateToken(data)
            };
        }


        private async Task<bool> UserExist(string usename)
        {
            return await _context.Users.AnyAsync(s=>s.UserName == usename.ToLower());
        }


    }
}
