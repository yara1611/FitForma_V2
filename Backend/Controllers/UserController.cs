using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly UserManager<User> _userManager; //what does the userManager do? manage the user identity table
    private readonly UserService _userService;
    public UserController(UserService userService, UserManager<User> userManager)
    {
        _userManager = userManager;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await _userManager.FindByIdAsync(id);
        if(user==null)
            return BadRequest();
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.ListAllUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            return Ok(user);
        
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        var newUser = new User
        {
            UserName = user.Email,
            Email = user.Email,
            Name = user.Name
        };
        var result = await _userManager.CreateAsync(newUser, "123Yara_");
        if (result.Succeeded)
        {
            return Ok(new
            {
                Message = "User registered successfully",
                UserId = newUser.Id // This will now show the integer ID
            });
        }
        //await _userService.CreateUserAsync(user); // i dont need this khalas

        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
       

        await _userService.DeleteUserAsync(id);
        return NoContent(); ;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user) //DTO later
    {
        if (id != user.Id)
        {
            return BadRequest("ID mismatch");
        }
        
            await _userService.UpdateUserAsync(user);
            return NoContent();
        
    }

}