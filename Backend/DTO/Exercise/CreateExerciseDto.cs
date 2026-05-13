public class ExerciseDto
{
    public string Name { get; set; } = null!;
    public string? TargetMuscle { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int RestSeconds { get; set; }
}