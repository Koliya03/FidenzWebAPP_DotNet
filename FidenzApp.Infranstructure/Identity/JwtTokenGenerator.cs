using FidenzApp.Application.Interfaces.Services.IServices;
using FidenzApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace FidenzApp.Infranstructure.Identity
{

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    public JwtTokenGenerator(IConfiguration config, UserManager<ApplicationUser> userManager)
    {
        _configuration = config;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
        new Claim("Department", user.Department)
    };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("AppSettings:validityInMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
}



