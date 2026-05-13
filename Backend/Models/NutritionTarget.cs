using System.ComponentModel.DataAnnotations;

public class NutritionTarget
{
    [Key]
    public int Id { get; set; }
    public int UserId {set;get;}
    public int Calories { get; set; }
    public double ProteinGrams { get; set; }
    public double CarbsGrams { get; set; }
    public double FatsGrams { get; set; }
    public double BMR {get;set;}
    public double TDEE {get;set;}
    public DateTime CreatedAt {get;set;}

    //Navigation Property
    public virtual User User { get; set; }
}