using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Services;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;

namespace TaskManagement.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO registerDto)
    {
        var result = await _authService.RegisterUserAsync(registerDto);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        var token = await _authService.LoginUserAsync(loginDto);
        if (token == null) return Unauthorized("Invalid credentials");

        return Ok(new { Token = token });
    }
}