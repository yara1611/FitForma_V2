public class UserService{
    
    private readonly IUserRepository _userRepo;
    public UserService(IUserRepository userRepo)
    {
        _userRepo=userRepo;
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
        await _userRepo.CreateUserProfile(pi);
        
    }
}