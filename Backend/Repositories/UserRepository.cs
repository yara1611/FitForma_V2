using Microsoft.EntityFrameworkCore;
public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task AddPersonalInfoAsync(PersonalInformation pi);
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

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();
    }
    public async Task AddPersonalInfoAsync(PersonalInformation pi)
    {
        await _context.PersonalInformation.AddAsync(pi);
        await _context.SaveChangesAsync(); // Ensures the info is saved to Post
    }
}
