using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PersonalInformation
{
    [Key]
    [ForeignKey("UserId")]
    public int UserId {set;get;}
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public DateTime DOB{set;get;}
    public char Gender{set;get;}
    public double Weight {set;get;}
    public double Height {set;get;}
    public string Goal {set;get;}
    public int TargetCalories { get; set; }
    public double TargetProtein { get; set; }
    public double TargetCarbs { get; set; }
    public double TargetFats { get; set; }

    public virtual User User { get; set; } // Back-reference
}