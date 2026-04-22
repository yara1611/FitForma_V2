public class MealService
{
    private readonly IMealRepository _mealRepo;
    public MealService(IMealRepository mealRepo)
    {
        _mealRepo = mealRepo;
    }

    #region Meal
    public async Task CreateMealAsync(Meal meal)
    {
        await _mealRepo.AddMealAsync(meal);
    }

    public async Task<Meal> GetMealAsync(int id)
    {
        return await _mealRepo.GetMealByIdAsync(id);
    }

    public async Task DeleteMealAsync(int id)
    {
        var meal = await _mealRepo.GetMealByIdAsync(id);
        if (meal == null)
        {
            throw new KeyNotFoundException("Meal not fount");

        }
        await _mealRepo.RemoveMealAsync(meal);
    }
    #endregion

    #region Plan
    public async Task CreatePlanAsync(MealPlan plan)
    {

        await _mealRepo.AddPlanAsync(plan);
    }
    public async Task DeleteFromPlanAsync(int planId, int mealId)
    {
        await _mealRepo.RemoveFromPlanAsync(planId,mealId);
    }

    public async Task DeletePlanAsync(int id)
    {
        //1- check exists
        var plan = await _mealRepo.GetPlanByIdAsync(id);
        if (plan == null)
            throw new KeyNotFoundException("Plan not found");

        var content = await _mealRepo.FindAllMealsInPlanAsync(id);

        if (content.Count != 0)
            //delete content (forloop then remove)
            await _mealRepo.RemoveMealsInPlanAsync(id);


        //3- delete plan
        await _mealRepo.RemovePlanAsync(plan);
    }

    public async Task AddMealToPlanAsync(int planId, Meal meal)
    {
        var plan = await _mealRepo.GetPlanByIdAsync(planId);
        if (plan == null)
            throw new KeyNotFoundException("Plan not found");

        if (plan.Meals == null)
            throw new InvalidOperationException("Plan meals not loaded");

        if (plan.Meals.Any(e => e.MealId == meal.MealId))
            throw new InvalidOperationException("Meal already exists in plan");
        plan.Meals.Add(meal);

        //update plan and save it
        await _mealRepo.UpdatePlanAsync(plan);
    }

    public async Task<List<Meal>> ListPlanContentAsync(int planId)
    {
        var plan = await _mealRepo.GetPlanByIdAsync(planId);
        if (plan == null)
        {
            throw new KeyNotFoundException("Plan not found");
        }
        var content = await _mealRepo.FindAllMealsInPlanAsync(planId);
        return content;
    }

    public async Task<MealPlan> GetPlanAsync(int id)
    {
        return await _mealRepo.GetPlanByIdAsync(id);
    }

    public async Task EditPlanAsync(MealPlan plan)
    {
        var existingPlan = await _mealRepo.GetPlanByIdAsync(plan.PlanId);
        if (existingPlan == null)
        {
            throw new KeyNotFoundException("Plan not found.");
        }
        await _mealRepo.UpdatePlanAsync(plan);
    }
    #endregion
}