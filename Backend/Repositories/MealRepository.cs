public interface IMealRepository
{
    public Task<MealPlan> GetMealPlanAsync(int planId);
}

public class MealRepository : IMealRepository
{
    public Task<MealPlan> GetMealPlanAsync(int planId)
    {
        throw new NotImplementedException();
    }
}