public class PlanResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } =null!;
    public string? Description { get; set; }
    public List<MealDto> Meals { get; set; } = new();
}