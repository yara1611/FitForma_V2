using System.ComponentModel.DataAnnotations;

public class User{

    [Key]
    public int UserId {set;get;}
    public string Name {set;get;}
    public string Email {set;get;}
    public string Password {set;get;} //will be hashed
    
    public DateTime CreatedAt {set;get;}
    public DateTime UpdatedAt {set;get;}

    // Navigation Properties
    public virtual PersonalInformation? PersonalInfo { get; set; }
    public virtual List<ExerciseRoutine> Routines { get; set; } = new();
    public virtual List<MealPlan> MealPlans { get; set; } = new();
}