using Microsoft.EntityFrameworkCore;

public interface IExerciseRepository
{
    Task<ExerciseRoutine> GetRoutineByIdAsync(int routineId);
    Task AddRoutineAsync(ExerciseRoutine routine);
    Task RemoveRoutineAsync(ExerciseRoutine routine);
    Task UpdateRoutineAsync(ExerciseRoutine routine);
    
    Task RemoveExercisesInRoutineAsync(int routineId);
    //Task<ExerciseRoutine> FindRoutineByName(string name)
    Task<List<ExerciseRoutine>> GetAllRoutinesByUserId(int userId);
    Task RemoveFromRoutineAsync(int routineId, int exerciseId);

    Task<Exercise> GetExerciseByIdAsync(int exerciseId);
    Task AddExerciseAsync(Exercise exercise);
    Task RemoveExerciseAsync(Exercise exercise);
    //Task UpdateExerciseAsync(ExerciseRoutine Routine);
    Task<List<Exercise>> GetAllExercisesAsync();
    Task<List<Exercise>> FindAllExercisesInRoutineAsync(int routineId);
}

public class ExerciseRepository : IExerciseRepository
{
    private readonly AppDbContext _context;
    public ExerciseRepository(AppDbContext context) => _context = context;

    #region Routine
    public async Task AddRoutineAsync(ExerciseRoutine routine)
    {
        // if (_context.Routines.FindAsync(routine.RoutineId) != null)
        // {
        //     throw new Exception("Already Exists");
        // }
        await _context.Routines.AddAsync(routine);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRoutineAsync(ExerciseRoutine routine)
    {
        _context.Routines.Remove(routine);
        await _context.SaveChangesAsync();
    }

    public async Task<ExerciseRoutine> GetRoutineByIdAsync(int routineId)
    {
        return await _context.Routines.FindAsync(routineId);
    }

    public async Task UpdateRoutineAsync(ExerciseRoutine routine)
    {
        _context.Routines.Update(routine);

        await _context.SaveChangesAsync();
    }


    #endregion

    #region Exercise
    public async Task AddExerciseAsync(Exercise exercise)
    {
        await _context.Exercises.AddAsync(exercise);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveExerciseAsync(Exercise exercise)
    {
        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();
    }

    public async Task<Exercise> GetExerciseByIdAsync(int exerciseId)
    {
        return await _context.Exercises.AsNoTracking()
        .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId);
    }

    public async Task<List<ExerciseRoutine>> GetAllRoutinesByUserId(int userId)
    {
        var exists = await _context.Users.AnyAsync(u => u.UserId == userId);

        if (!exists)
            throw new KeyNotFoundException("Routine not found");
        //as no tracking faster and less memory
        return await _context.Routines.Where(r => r.UserId == userId).AsNoTracking().ToListAsync();
    }


    public async Task<List<Exercise>> GetAllExercisesAsync()
    {
        return await _context.Exercises.ToListAsync();
    }

    //get all Exercises with RoutineId
    public async Task<List<Exercise>> FindAllExercisesInRoutineAsync(int routineId)
    {
        var exists = await _context.Routines.AnyAsync(r => r.RoutineId == routineId);

        if (!exists)
            throw new KeyNotFoundException("Routine not found");
        //ass no tracking faster and less memory
        return await _context.Exercises.Where(m => m.RoutineId == routineId).AsNoTracking().ToListAsync();
    }

    public async Task RemoveExercisesInRoutineAsync(int routineId)
    {
        await _context.Exercises.Where(e => e.RoutineId == routineId).ExecuteDeleteAsync();

    }

    public async Task RemoveFromRoutineAsync(int routineId, int exerciseId)
    {
        var exercise = await _context.Exercises
        .FirstOrDefaultAsync(e => e.RoutineId == routineId && e.ExerciseId == exerciseId);
        if (exercise == null)
            throw new KeyNotFoundException("Exercise not found in routine");

        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();

    }
    #endregion

}