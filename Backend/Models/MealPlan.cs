using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MealPlan
{
    [Key]
    public int PlanId {get;set;}
    [ForeignKey("UserId")]
    public int UserId {get;set;}
    public string Name {set;get;}
    public string Description {set;get;}
    public DateTime CreatedAt {set;get;}

    //Navigation Properties
    public virtual User User { get; set; }
    public virtual List<Meal> Meals { get; set; } = new();

}