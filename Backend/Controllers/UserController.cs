using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//auth the class and whitelist public endpoints if needed.
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly UserManager<User> _userManager; //what does the userManager do? manage the user identity table
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly NutritionService _nutritionService;
    public UserController(UserService userService, UserManager<User> userManager, IMapper mapper, NutritionService nutritionService)
    {
        _userManager = userManager;
        _userService = userService;
        _mapper=mapper;
        _nutritionService=nutritionService;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id == null)
        {
            return Unauthorized("User ID not found in token.");
        }
        var user = await _userManager.FindByIdAsync(id);
        if(user==null)
            return BadRequest();
        return Ok(_mapper.Map<UserResponseDto>(user));
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.ListAllUsersAsync();
       
        return Ok( _mapper.Map<List<UserResponseDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            return Ok( _mapper.Map<UserResponseDto>(user));
        
    }

    // [HttpPost]
    // public async Task<IActionResult> CreateUser(User user)
    // {
    //     var newUser = new User
    //     {
    //         UserName = user.Email,
    //         Email = user.Email,
    //         Name = user.Name
    //     };
    //     var result = await _userManager.CreateAsync(newUser, "123Yara_");
    //     if (result.Succeeded)
    //     {
    //         return Ok(new
    //         {
    //             Message = "User registered successfully",
    //             UserId = newUser.Id // This will now show the integer ID
    //         });
    //     }
    //     //await _userService.CreateUserAsync(user); // i dont need this khalas

    //     return BadRequest(result.Errors);
    // }

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

    [HttpPost("{id}/profile")]
    public async Task<IActionResult> CreateUserProfile(int id,UserProfile pi)
    {
        await _userService.CreateUserProfileAsync(id,pi);
        return Ok();
    }
    [HttpGet("{id}/profile")]
    public async Task<IActionResult> GetProfile()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id == null)
        {
            return Unauthorized("User ID not found in token.");
        }
        return Ok(await _userService.GetUserProfileAsync(int.Parse(id)));
    }

    //PUT /user/profile   → update profile, recalculate targets
    [HttpPut("{id}/profile")]
    public async Task<IActionResult> UpdateProfile([FromBody]UserProfileDto userProfile) //why dto? to avoid overposting and to have more control over the data that is being updated
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id == null)
        {
            return Unauthorized("User ID not found in token.");
        }
        await _userService.UpdateUserProfileAsync(int.Parse(id), userProfile);
        return Ok();
    }

    //GET /user/nutrition — return nutrition targets
    [HttpGet("{id}/nutrition")]
    public async Task<IActionResult> GetTargets(int id)
    {
        //await _userService.GetUserProfileAsync(id);
        return Ok(await _nutritionService.GetTargetsOfUserAsync(id));
    }
    
}