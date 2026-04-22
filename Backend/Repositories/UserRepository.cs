using Microsoft.EntityFrameworkCore;
public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
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

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if(user==null) throw new KeyNotFoundException("User not Found");
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
    public async Task AddPersonalInfoAsync(PersonalInformation pi)
    {
        var exists = await _context.PersonalInformation.AnyAsync(i=>i.UserId==pi.UserId);
        if(exists)
            throw new InvalidOperationException("Personal info already exists for this user");
        await _context.PersonalInformation.AddAsync(pi);
        await _context.SaveChangesAsync(); // Ensures the info is saved to Post
    }
}
