public class NutritionService
{
    private readonly INutritionRepository _nutritionRepo;
    private readonly NutritionCalculator _calculator = new NutritionCalculator();
    public NutritionService(INutritionRepository nutritionRepo, NutritionCalculator calculator)
    {
        _nutritionRepo = nutritionRepo;
        _calculator = calculator;
    }


    // Call this after saving a UserProfile
    public async Task CalculateAndSaveTargetAsync(UserProfile profile)
    {
        var target = _calculator.CalculateTarget(profile);
        await _nutritionRepo.AddTargetAsync(target);
    }

    public async Task SaveTargetAsync(NutritionTarget target)
    {
        await _nutritionRepo.AddTargetAsync(target);
    }

    public async Task<NutritionTarget> GetTargetAsync(int id)
    {
        return await _nutritionRepo.GetByIdAsync(id);
    }

    public async Task<List<NutritionTarget>> GetTargetsOfUserAsync(int userId)
    {
        return await _nutritionRepo.GetAllByUserIdAsync(userId);
    }

    
}