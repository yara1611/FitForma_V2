using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { set; get; }
    public DbSet<ExerciseRoutine> Routines { set; get; }
    public DbSet<Exercise> Exercises { set; get; }
    public DbSet<MealPlan> Plans { set; get; }
    public DbSet<Meal> Meals { set; get; }
    public DbSet<NutritionTarget> NutritionTargets { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //User -> UP (1-1)
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserProfile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //User -> Routines (1-M)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Routines)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //User -> Plans (1-M)
        modelBuilder.Entity<User>()
            .HasMany(u => u.MealPlans)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //User -> Nutrition Target (1-M)
        modelBuilder.Entity<User>()
            .HasMany(u => u.NutritionTargets)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //Plans -> Meal (1-M)
        modelBuilder.Entity<MealPlan>()
            .HasMany(p => p.Meals)
            .WithOne(m => m.Plan)
            .HasForeignKey(m => m.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
        //Routines -> Exercise (1-M)
        modelBuilder.Entity<ExerciseRoutine>()
            .HasMany(r => r.Exercises)
            .WithOne(e => e.Routine)
            .HasForeignKey(e => e.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProfile>()
            .Property(x => x.Goal)
            .HasConversion<string>();

        //save enum as string instead of int 
        modelBuilder.Entity<UserProfile>()
        .Property(x => x.ActivityLevel)
        .HasConversion<string>();

    }

}
