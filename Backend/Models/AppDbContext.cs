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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //User -> PI (1-1)
        modelBuilder.Entity<User>()
            .HasOne(u=>u.PersonalInfo)
            .WithOne(p=>p.User)
            .HasForeignKey<PersonalInformation>(p=>p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //User -> Routines (1-M)
        modelBuilder.Entity<User>()
            .HasMany(u=>u.Routines)
            .WithOne(r=>r.User)
            .HasForeignKey(r=>r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //User -> Plans (1-M)
        modelBuilder.Entity<User>()
            .HasMany(u=>u.MealPlans)
            .WithOne(p=>p.User)
            .HasForeignKey(p=>p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        //Plans -> Meal (1-M)
        modelBuilder.Entity<MealPlan>()
            .HasMany(p=>p.Meals)
            .WithOne(m=>m.Plan)
            .HasForeignKey(m=>m.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
        //Routines -> Exercise (1-M)
        modelBuilder.Entity<ExerciseRoutine>()
            .HasMany(r=>r.Exercises)
            .WithOne(e=>e.Routine)
            .HasForeignKey(e=>e.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}
