using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class User: IdentityUser<int>
{

    [Key]
    public int UserId {set;get;}
    public string Name {set;get;} = null!;
    //public string Email {set;get;} = null!;
    //public string Password {set;get;}  = null!;//will be hashed
    
    public DateTime CreatedAt {set;get;}= DateTime.UtcNow;
    public DateTime UpdatedAt {set;get;}= DateTime.UtcNow;

    // Navigation Properties
    public virtual PersonalInformation? PersonalInfo { get; set; }
    public virtual List<ExerciseRoutine> Routines { get; set; } = new();
    public virtual List<MealPlan> MealPlans { get; set; } = new();
}