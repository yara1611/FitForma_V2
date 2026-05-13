using Microsoft.EntityFrameworkCore;

public interface INutritionRepository
{
    Task<NutritionTarget> GetByIdAsync(int id);
    Task<List<NutritionTarget>> GetAllByUserIdAsync(int userId);
    Task AddTargetAsync(NutritionTarget target);
}
public class NutritionRepository : INutritionRepository
{

    private readonly AppDbContext _context;
    public NutritionRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task AddTargetAsync(NutritionTarget target)
    {
        _context.NutritionTargets.AddAsync(target);
        await _context.SaveChangesAsync();
    }

    public async Task<List<NutritionTarget>> GetAllByUserIdAsync(int userId)
    {
        var exists = await _context.Users.AnyAsync(u=>u.Id==userId);
        if (!exists)
            throw new KeyNotFoundException("User not found");
        return await _context.NutritionTargets.Where(n=>n.UserId==userId).AsNoTracking().ToListAsync();
    }

    public async Task<NutritionTarget> GetByIdAsync(int id)
    {
        return await _context.NutritionTargets.FindAsync(id);
    }
}