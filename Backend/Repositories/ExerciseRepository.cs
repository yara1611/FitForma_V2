using Microsoft.EntityFrameworkCore;

public interface IExerciseRepository
{
    Task<ExerciseRoutine> GetExerciseRoutineAsync(int routineId);
    Task AddRoutineAsync(ExerciseRoutine routine);
    Task RemoveRoutineAsync(ExerciseRoutine routine);
    Task UpdateRoutineAsync(ExerciseRoutine routine);
    Task<IEnumerable<ExerciseRoutine>> GetAllRoutinesAsync();
    //Task<ExerciseRoutine> FindRoutineByName(string name)

    Task<Exercise> GetExerciseAsync(int exerciseId);
    Task AddExerciseAsync(Exercise exercise);
    Task RemoveExerciseAsync(Exercise Exercise);
    //Task UpdateExerciseAsync(ExerciseRoutine Routine);
    Task<IEnumerable<Exercise>> GetAllExercisesAsync();
    Task<IEnumerable<Exercise>> FindAllExercisesInRoutine(int routineId);
}

public class ExerciseRepository : IExerciseRepository
{
    private readonly AppDbContext _context;
    public ExerciseRepository(AppDbContext context) => _context = context;

    #region Routine
    public async Task AddRoutineAsync(ExerciseRoutine routine)
    {
        await _context.Routines.AddAsync(routine);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRoutineAsync(ExerciseRoutine routine)
    {
        _context.Routines.Remove(routine);
        await _context.SaveChangesAsync();
    }

    public async Task<ExerciseRoutine> GetExerciseRoutineAsync(int routineId)
    {
        return await _context.Routines.FindAsync(routineId);
    }

    public async Task UpdateRoutineAsync(ExerciseRoutine routine)
    {
        _context.Routines.Update(routine);
        
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ExerciseRoutine>> GetAllRoutinesAsync()
    {
        return await _context.Routines.ToListAsync();
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

    public async Task<Exercise> GetExerciseAsync(int exerciseId)
    {
        return await _context.Exercises.FindAsync(exerciseId);
    }


    public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
    {
        return await _context.Exercises.ToListAsync();
    }

    //get all Exercises with RoutineId
    public async Task<IEnumerable<Exercise>> FindAllExercisesInRoutine(int routineId)
    {
        return await _context.Exercises.Where(m => m.RoutineId==routineId).ToListAsync();
    }

    
    #endregion

}