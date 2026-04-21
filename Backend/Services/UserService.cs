public class UserService{
    
    private readonly IUserRepository _userRepo;
    public UserService(IUserRepository userRepo)
    {
        _userRepo=userRepo;
    }
    
    public async Task CreateUser(User user)
    {
        await _userRepo.AddAsync(user);
        
    }

    public async Task<List<User>> ListAllUsers()
    {
        var users = await _userRepo.GetAllAsync();
        return users.ToList();
    }
    public async Task<User> ListUser(int id)
    {
        var user =await _userRepo.GetByIdAsync(id);
        if (user==null)
        {
            throw new Exception("User not found");
        }
        return user;
    }

    public async Task DeleteUser(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user==null)
        {
            throw new Exception("User not found");
        }
       await _userRepo.DeleteAsync(user);
    }

    //PERSONAL INFO
    public async Task CreatePersonalInfoAsync(int id,PersonalInformation pi)
    {
        var user = _userRepo.GetByIdAsync(id);
        if (user == null) throw new Exception("User not found");
        pi.UserId = id;
        await _userRepo.AddPersonalInfoAsync(pi);
        
    }
}