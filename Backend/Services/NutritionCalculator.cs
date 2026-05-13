public class NutritionCalculator
{
    public NutritionTarget CalculateTarget(UserProfile user)
    {
        if (user.Weight <= 0 || user.Height <= 0)
            throw new ArgumentException("Invalid body metrics");

        if (!Enum.IsDefined(typeof(ActivityLevel), user.ActivityLevel))
            throw new ArgumentException("Invalid activity level");
        var age = CalculateAge(user.DOB);
        var bmr = CalculateBMR(user.Weight, user.Height, age, user.Gender);
        var tdee = CalculateTDEE(bmr, user.ActivityLevel);
        var calories = CalculateCalories(tdee, user.Goal);
        var macros = CalculateMacros(calories, user.Weight, user.Goal);
        //protein intake = 1.6 to 2.2w
        //fat intake = 0.8to1.0W
        //carbs = (cal-(p*4+f*9))/4
        //optional bodyfat adjustment 
        //lbm = w-(1-BF%) -> BMR = 370+21.6*LBM (Katch-McArdle formula)
        return new NutritionTarget
        {
            UserId = user.UserId,
            Calories = (int)Math.Round(calories),
            ProteinGrams = Math.Round(macros.ProteinGrams, 1),
            FatsGrams = Math.Round(macros.FatGrams, 1),
            CarbsGrams = Math.Round(macros.CarbsGrams, 1),
            BMR = bmr,
            TDEE = tdee,
            CreatedAt = DateTime.UtcNow
        };
    }

    //kcal per gram
    public double CalculateBMR(double weight, double height, int age, string gender)
    {

        var bmr = 10 * weight + 6.25 * height - 5 * age;
        if (gender.ToLower() == "female")
        {
            bmr -= 161;
        }
        else if (gender.ToLower() == "male")
        {
            bmr += 5;
        }
        return bmr;
    }

    public double CalculateTDEE(double bmr, ActivityLevel activity)
    {
        var TDEE = bmr;
        //replave with enums
        return activity switch
        {
            ActivityLevel.Sedentary => TDEE * 1.2,
            ActivityLevel.Light => TDEE * 1.375,
            ActivityLevel.Moderate => TDEE * 1.55,
            ActivityLevel.Hard => TDEE * 1.725,
            ActivityLevel.Athlete => TDEE * 1.9,
            _ => throw new ArgumentException("Invalid activity level"),
        };
    }
    public double CalculateCalories(double tdee, Goal goal)
    {

        return goal switch
        {
            Goal.CUT => tdee * 0.85,
            Goal.BULK => tdee * 1.10,
            Goal.MAINTAIN => tdee,
            _ => throw new ArgumentException("Invalid goal")
        };
    }

    //macros 
    public MacroResult CalculateMacros(double calories, double weight, Goal goal)
    {
        double proteinPerKg = goal switch
        {
            Goal.CUT => 2.2,
            Goal.BULK => 1.8,
            Goal.MAINTAIN => 1.6,
            _ => throw new ArgumentException("Invalid goal")
        };

        double fatPerKg = goal switch
        {
            Goal.CUT => 0.7,
            Goal.BULK => 0.9,
            Goal.MAINTAIN => 0.8,
            _ => throw new ArgumentException("Invalid goal")
        };

        double protein = proteinPerKg * weight;
        double fat = fatPerKg * weight;

        double proteinCalories = protein * 4;
        double fatCalories = fat * 9;

        double carbs = (calories - (proteinCalories + fatCalories)) / 4;

        if (carbs < 0) carbs = 0;

        return new MacroResult
        {
            ProteinGrams = protein,
            FatGrams = fat,
            CarbsGrams = carbs
        };
    }

    public int CalculateAge(DateTime dob)
    {
        var today = DateTime.Today;

        int age = today.Year - dob.Year;

        if (dob.Date > today.AddYears(-age))
            age--;

        return age;
    }

}