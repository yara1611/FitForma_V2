using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserProfile
{
    [Key]
    public int Id {get;set;}
    public int UserId {set;get;}
    public string FirstName {get;set;} = null!;
    public string LastName {get;set;} = null!;
    public DateTime DOB{set;get;}
    public string Gender { get; set; } = "Unknown";
    public double Weight {set;get;}
    public double Height {set;get;}
    public Goal Goal {set;get;}
    public ActivityLevel ActivityLevel {set;get;}
    public double? BodyFatPercent {set;get;}
    

    public virtual User? User { get; set; } // Back-reference
}