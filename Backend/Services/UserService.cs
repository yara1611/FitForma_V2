using AutoMapper;

public class UserService{
    
    private readonly IUserRepository _userRepo;
    private readonly NutritionService _nutritionService;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepo, NutritionService nutritionService, IMapper mapper)
    {
        _userRepo=userRepo;
        _nutritionService=nutritionService;
        _mapper=mapper;
    }
    
    public async Task CreateUserAsync(User user)
    {
        await _userRepo.AddAsync(user);
           
    }

    

    public async Task<List<User>> ListAllUsersAsync()
    {
        var users = await _userRepo.GetAllAsync();
        return users.ToList();
    }
    public async Task<User> ListUserAsync(int id)
    {
        var user =await _userRepo.GetByIdAsync(id);
        if (user==null)
        {
            throw new Exception("User not found");
        }
        return user;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        await _userRepo.DeleteAsync(user);
    }


    public async Task UpdateUserAsync(User user)
    {
        var exisitingUser = await _userRepo.GetByIdAsync(user.Id);
        if (exisitingUser == null)
        {
            throw new Exception("User not found.");
        }
        exisitingUser.Name=user.Name;
        exisitingUser.Email=user.Email;
        await _userRepo.UpdateAsync(user);
    }

    //PERSONAL INFO
    public async Task CreateUserProfileAsync(int id,UserProfile pi)
    {
        var user = await _userRepo.GetByIdAsync(id) ?? throw new Exception("User not found");
        var exists = await _userRepo.UserProfileExists(pi.Id);
        if(exists) throw new Exception("User Profile already exists");
        pi.UserId = id;
        pi.DOB = DateTime.SpecifyKind(pi.DOB, DateTimeKind.Utc);
        await _userRepo.CreateUserProfile(pi);
        await _nutritionService.CalculateAndSaveTargetAsync(pi);
        
    }

    internal async Task<UserProfile> GetUserProfileAsync(int id)
    {
        
        var exists = await _userRepo.UserProfileExists(id);
        if(!exists) throw new Exception("User Profile not found");
        return await _userRepo.GetUserProfileAsync(id);
    }

    public async Task UpdateUserProfileAsync(int userId, UserProfileDto dto)
    {
        var existingProfile = await _userRepo.GetUserProfileAsync(userId);
        if (existingProfile == null)
        {
            throw new KeyNotFoundException("Profile not found.");
        }
        _mapper.Map(dto, existingProfile);
        await _userRepo.UpdateProfileAsync(existingProfile);
        await _nutritionService.CalculateAndSaveTargetAsync(existingProfile);
    }
}