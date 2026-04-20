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
    public IActionResult GetUsers()
    {
        return Ok(_userService.ListAllUsers());
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok(_userService.ListUser(id));
    }

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        _userService.CreateUser(user);
        
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return Ok("Deleted");
    }
     
}