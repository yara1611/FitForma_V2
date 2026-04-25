using AutoMapper;
public interface IMealService
{
    Task CreateMealAsync(Meal meal);
    Task<Meal> GetMealAsync(int id);
    Task DeleteMealAsync(int id);
    Task CreatePlanAsync(MealPlan plan, int userId);
    Task DeletePlanAsync(int id);
    Task AddMealToPlanAsync(int planId, Meal meal);
    Task<List<Meal>> ListPlanContentAsync(int planId);
    Task<MealPlan> GetPlanAsync(int id);
    Task EditPlanAsync(int id, UpdatePlanDto dto);
    Task<List<MealPlan>> GetAllPlansByUserId(int userId);
}
public class MealService:IMealService
{
    private readonly IMealRepository _mealRepo;
    private readonly IMapper _mapper;
    public MealService(IMealRepository mealRepo,IMapper mapper)
    {
        _mealRepo = mealRepo;
        _mapper=mapper;
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
    public async Task CreatePlanAsync(MealPlan plan, int userId)
    {
        plan.UserId = userId;
        await _mealRepo.AddPlanAsync(plan);
    }

    public async Task DeletePlanAsync(int id)
    {
        //1- check exists
        var plan = await _mealRepo.GetPlanByIdAsync(id);
        if (plan == null)
            throw new KeyNotFoundException("Plan not found");

        var content = await _mealRepo.FindAllMealsInPlanAsync(id);

        if (content.Count != 0)
            //delete content
            await _mealRepo.RemoveMealsInPlanAsync(id);


        //3- delete plan
        await _mealRepo.RemovePlanAsync(plan);
    }

    public async Task AddMealToPlanAsync(int planId, Meal meal)
    {
        var plan = await _mealRepo.GetPlanByIdAsync(planId);

        if (plan == null)
            throw new KeyNotFoundException("Plan not found");

        // attach meal to plan
        meal.PlanId = planId;

        await _mealRepo.AddMealAsync(meal);
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

    public async Task EditPlanAsync(int id, UpdatePlanDto dto)
    {
        var existingPlan = await _mealRepo.GetPlanByIdAsync(id);
        if (existingPlan == null)
            throw new KeyNotFoundException("Plan not found.");
        _mapper.Map(dto,existingPlan);
        existingPlan.UpdatedAt = DateTime.UtcNow;
        await _mealRepo.UpdatePlanAsync(existingPlan);
    }


    public async Task<List<MealPlan>> GetAllPlansByUserId(int userId)
    {
        var list = await _mealRepo.GetAllPlansByUserId(userId);
        return list;
    }

    #endregion
}