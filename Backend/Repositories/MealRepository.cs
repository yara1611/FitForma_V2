using Microsoft.EntityFrameworkCore;

public interface IMealRepository
{
    Task<MealPlan> GetMealPlanAsync(int planId);
    Task AddPlanAsync(MealPlan plan);
    Task RemovePlanAsync(MealPlan plan);
    Task UpdatePlanAsync(MealPlan plan);
    Task<IEnumerable<MealPlan>> GetAllPlansAsync();
    //Task<MealPlan> FindPlanByName(string name)

    Task<Meal> GetMealAsync(int mealdId);
    Task AddMealAsync(Meal meal);
    Task RemoveMealAsync(Meal meal);
    //Task UpdateMealAsync(MealPlan plan);
    Task<IEnumerable<Meal>> GetAllMealsAsync();
    Task<IEnumerable<Meal>> FindAllMealsInPlan(int planId);
}

public class MealRepository : IMealRepository
{
    private readonly AppDbContext _context;
    public MealRepository(AppDbContext context) => _context = context;

    #region PLAN
    public async Task AddPlanAsync(MealPlan plan)
    {
        await _context.Plans.AddAsync(plan);
        await _context.SaveChangesAsync();
    }

    public async Task RemovePlanAsync(MealPlan plan)
    {
        _context.Plans.Remove(plan);
        await _context.SaveChangesAsync();
    }

    public async Task<MealPlan> GetMealPlanAsync(int planId)
    {
        return await _context.Plans.FindAsync(planId);
    }

    public async Task UpdatePlanAsync(MealPlan plan)
    {
        _context.Plans.Update(plan);
        
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MealPlan>> GetAllPlansAsync()
    {
        return await _context.Plans.ToListAsync();
    }
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

    public async Task<Meal> GetMealAsync(int mealId)
    {
        return await _context.Meals.FindAsync(mealId);
    }


    public async Task<IEnumerable<Meal>> GetAllMealsAsync()
    {
        return await _context.Meals.ToListAsync();
    }

    //get all meals with planId
    public async Task<IEnumerable<Meal>> FindAllMealsInPlan(int planId)
    {
        return await _context.Meals.Where(m => m.PlanId==planId).ToListAsync();
    }

    
    #endregion

}