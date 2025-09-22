using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.Services.IServices;
using FidenzApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.Interfaces.Services.services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
           
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Department = registerDto.Department
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            await _userManager.AddToRoleAsync(user, registerDto.Role);
            return true;
        }

        public async Task<String?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return null;
            }
            

            var token =await _jwtTokenGenerator.GenerateToken(user);
            return  token;
        }
    }
}
