using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly UserManager<User> _userManager;
    public AuthController(AuthService authService, UserManager<User> userManager)
    {
        _authService = authService;
        _userManager = userManager;
    }


    //TEST: Test, test, Test@123
    //register
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto) //reg dto later
    {
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            Name = dto.Name
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var token = _authService.GenerateToken(user);
        return Ok(new
        {
            Token = token,
            Message = "Registration successful"
        });
    }

    //login
    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return Unauthorized("Invalid username or password");
        }
        var token = _authService.GenerateToken(user);
        return Ok(new {
            Token = token,
            Message = "Login successful"
        });
    }
}