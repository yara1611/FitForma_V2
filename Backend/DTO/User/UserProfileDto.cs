public class UserProfileDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DOB { get; set; }
    public string Gender { get; set; } = null!;
    public double Weight { get; set; }
    public double Height { get; set; }
    public Goal Goal { get; set; }
    public ActivityLevel ActivityLevel { get; set; }
    public double? BodyFatPercent { get; set; }
}