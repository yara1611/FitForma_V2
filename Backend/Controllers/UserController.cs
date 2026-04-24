using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly UserManager<User> _userManager;
    private readonly UserService _userService;
    public UserController(UserService userService,UserManager<User> userManager)
    {
        _userManager=userManager;
        _userService=userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.ListAllUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
    {
        return NotFound($"User with ID {id} was not found.");
    }
            return Ok(user);
        }
        catch(Exception)
        {
            return NotFound("User not found or has been deleted.");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        var newUser = new User
        {
            UserName=user.Email,
            Email=user.Email,
            Name=user.Name
        };
        var result = await _userManager.CreateAsync(newUser,"123Yara_");
        if (result.Succeeded)
        {
            return Ok(new { 
            Message = "User registered successfully", 
            UserId = newUser.Id // This will now show the integer ID (e.g., 1, 2, 3)
        });
        }
        //await _userService.CreateUserAsync(user);
        
        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        
        await _userService.DeleteUserAsync(id);
        return NoContent();;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user) //DTO later
    {
        if (id != user.UserId)
        {
            return BadRequest("ID mismatch");
        }
        try
        {
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            
            return NotFound(e.Message);
        }
    }
     
}