using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MealPlan
{
    [Key]
    public int PlanId {get;set;}
    public int UserId {get;set;}
    public string Name {set;get;}=null!;
    public string? Description {set;get;}
    public DateTime CreatedAt {set;get;} = DateTime.UtcNow;
    public DateTime UpdatedAt {set;get;} = DateTime.UtcNow;

    //Navigation Properties
    public virtual User User { get; set; }= null!;
    public virtual List<Meal> Meals { get; set; } = new();

}