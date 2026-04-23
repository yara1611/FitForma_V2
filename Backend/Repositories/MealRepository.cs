using Microsoft.EntityFrameworkCore;

public interface IMealRepository
{
    //Why async?
    /*
    Repository methods are async because they perform I/O operations (database calls) 
    and async prevents blocking threads, improving scalability in web APIs
    */
    Task<MealPlan> GetPlanByIdAsync(int planId);
    Task AddPlanAsync(MealPlan plan);
    Task RemovePlanAsync(MealPlan plan);
    Task UpdatePlanAsync(MealPlan plan);
    //Task<List<MealPlan>> GetAllPlansAsync();
    Task RemoveMealsInPlanAsync(int planId);
    //Task<MealPlan> FindPlanByName(string name)
    Task<List<MealPlan>> GetAllPlansByUserId(int userId);
    Task RemoveFromPlanAsync(int planId, int mealId);

    Task<Meal> GetMealByIdAsync(int mealId);
    Task AddMealAsync(Meal meal);
    Task RemoveMealAsync(Meal meal);
    //Task UpdateMealAsync(MealPlan Plan);
    Task<List<Meal>> GetAllMealsAsync();
    Task<List<Meal>> FindAllMealsInPlanAsync(int planId);
}

public class MealRepository : IMealRepository
{
    private readonly AppDbContext _context;
    public MealRepository(AppDbContext context) => _context = context;

    #region Plan
    public async Task AddPlanAsync(MealPlan plan)
    {
        // if (_context.Plans.FindAsync(plan.PlanId) != null)
        // {
        //     throw new Exception("Already Exists");
        // }
        await _context.Plans.AddAsync(plan);
        await _context.SaveChangesAsync();
    }

    public async Task RemovePlanAsync(MealPlan plan)
    {
        _context.Plans.Remove(plan);
        await _context.SaveChangesAsync();
    }

    public async Task<MealPlan> GetPlanByIdAsync(int planId)
    {
        return await _context.Plans.FindAsync(planId);
    }

    public async Task UpdatePlanAsync(MealPlan plan)
    {
        _context.Plans.Update(plan);

        await _context.SaveChangesAsync();
    }

    // public async Task<List<MealPlan>> GetAllPlansAsync()
    // {
    //     return await _context.Plans.AsNoTracking().ToListAsync();
    // }

    #endregion

    #region Meal
    public async Task AddMealAsync(Meal meal)
    {
        await _context.Meals.AddAsync(meal);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveMealAsync(Meal meal)
    {
        _context.Meals.Remove(meal);
        await _context.SaveChangesAsync();
    }

    public async Task<Meal> GetMealByIdAsync(int mealId)
    {
        return await _context.Meals.AsNoTracking()
        .FirstOrDefaultAsync(e => e.MealId == mealId);
    }

    public async Task<List<MealPlan>> GetAllPlansByUserId(int userId)
    {
        var exists = await _context.Users.AnyAsync(u => u.UserId == userId);

        if (!exists)
            throw new KeyNotFoundException("Plan not found");
        //as no tracking faster and less memory
        return await _context.Plans.Where(r => r.UserId == userId).AsNoTracking().ToListAsync();
    }


    public async Task<List<Meal>> GetAllMealsAsync()
    {
        return await _context.Meals.ToListAsync();
    }

    //get all Meals with PlanId
    public async Task<List<Meal>> FindAllMealsInPlanAsync(int planId)
    {
        var exists = await _context.Plans.AnyAsync(r => r.PlanId == planId);

        if (!exists)
            throw new KeyNotFoundException("Plan not found");
        //ass no tracking faster and less memory
        return await _context.Meals.Where(m => m.PlanId == planId).AsNoTracking().ToListAsync();
    }

    public async Task RemoveMealsInPlanAsync(int planId)
    {
        await _context.Meals.Where(e => e.PlanId == planId).ExecuteDeleteAsync();

    }

    public async Task RemoveFromPlanAsync(int planId, int mealId)
    {
        var meal = await _context.Meals
        .FirstOrDefaultAsync(e => e.PlanId == planId && e.MealId == mealId);
        if (meal == null)
            throw new KeyNotFoundException("Meal not found in plan");

        _context.Meals.Remove(meal);
        await _context.SaveChangesAsync();

    }


    #endregion

}