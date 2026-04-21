using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<PersonalInformation> PersonalInformation {set;get;}
    public DbSet<ExerciseRoutine> Routines{set;get;}
    public DbSet<Exercise> Exercises{set;get;}
    public DbSet<MealPlan> Plans {set;get;}
    public DbSet<Meal> Meals{set;get;}
    
}
