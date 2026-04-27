using Microsoft.EntityFrameworkCore;
public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task CreateUserProfile(UserProfile pi);
    Task<bool> UserProfileExists(int id);
}
public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {   
        
        return await _context.Users.FindAsync(id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();
    }
    public async Task CreateUserProfile(UserProfile pi)
    {
        await _context.UserProfiles.AddAsync(pi);
        await _context.SaveChangesAsync(); // Ensures the info is saved to Post
    }

    public async Task<bool> UserProfileExists(int id)
    {
        return await _context.UserProfiles.AnyAsync(i=>i.UserId==id);
    }
}
