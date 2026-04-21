using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService=userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.ListAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        try
        {
            var user = await _userService.ListUser(id);
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
        await _userService.CreateUser(user);
        
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        
        await _userService.DeleteUser(id);
        return NoContent();;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.UserId)
        {
            return BadRequest("ID mismatch");
        }
        try
        {
            await _userService.UpdateUser(user);
            return NoContent();
        }
        catch (System.Exception e)
        {
            
            return NotFound(e.Message);
        }
    }
     
}