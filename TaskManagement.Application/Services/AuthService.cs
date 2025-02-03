using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.DTOs; 

namespace TaskManagement.API.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>(); 
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterDTO registerDto)
    {
        var user = new ApplicationUser { UserName = registerDto.Username, Email = registerDto.Email };
        return await _userManager.CreateAsync(user, registerDto.Password);
    }

    public async Task<string?> LoginUserAsync(LoginDTO loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null) return null;

        var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (!result.Succeeded) return null;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
