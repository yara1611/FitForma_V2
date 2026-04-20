using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Meal
{
    [Key]
    public int MealId {get;set;}
    [ForeignKey("PlanId")]
    public int PlanId {get;set;}
    public string Name {get;set;}
    public double Calories {get;set;}
    public double Protein {get;set;}
    public double Fats {get;set;}
    public double Carbs {get;set;}

    //Navigation Properties
    public virtual MealPlan Plan { get; set; }
}
