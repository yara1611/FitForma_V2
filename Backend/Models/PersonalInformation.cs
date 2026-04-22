using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PersonalInformation
{
    [Key]
    public int UserId {set;get;}
    public string FirstName {get;set;} = null!;
    public string LastName {get;set;} = null!;
    public DateTime DOB{set;get;}
    public string Gender { get; set; } = "Unknown";
    public double Weight {set;get;}
    public double Height {set;get;}
    public string? Goal {set;get;}
    public int TargetCalories { get; set; }
    public double TargetProtein { get; set; }
    public double TargetCarbs { get; set; }
    public double TargetFats { get; set; }

    public virtual User User { get; set; } // Back-reference
}