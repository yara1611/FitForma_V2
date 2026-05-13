public class RoutineResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<ExerciseDto> Exercises { get; set; } = new();
}