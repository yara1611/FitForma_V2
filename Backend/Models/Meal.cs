using System.ComponentModel.DataAnnotations;

public class Meal
{
    [Key]
    public int MealId {get;set;}
    public int PlanId {get;set;}
    public string Name {get;set;}= null!;
    [Range(0, 5000)]
    public double Calories {get;set;}
    [Range(0, 5000)]
    public double Protein {get;set;}
    [Range(0, 5000)]
    public double Fats {get;set;}
    [Range(0, 5000)]
    public double Carbs {get;set;}

    //Navigation Properties
    public virtual MealPlan Plan { get; set; }= null!;
}
