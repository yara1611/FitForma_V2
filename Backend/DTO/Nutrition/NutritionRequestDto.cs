public class NutritionRequestDto
{
    public double Weight { get; set; }
    public double Height { get; set; }
    public DateTime DOB { get; set; }
    public string  Gender { get; set; }
    public Goal Goal { get; set; }
    public ActivityLevel ActivityLevel { get; set; }
}