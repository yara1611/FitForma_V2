public class CreateMealDto
{
    public int PlanId {get;set;}
    public string Name {get;set;}= null!;
    public double Calories {get;set;}
    public double Protein {get;set;}
    public double Fats {get;set;}
    public double Carbs {get;set;}
}