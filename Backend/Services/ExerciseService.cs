public class ExerciseService
{
    private readonly IExerciseRepository _exerciseRepo;
    public ExerciseService(IExerciseRepository exerciseRepo)
    {
        _exerciseRepo = exerciseRepo;
    }

    #region Exercise
    public async Task CreateExerciseAsync(Exercise exercise)
    {
        await _exerciseRepo.AddExerciseAsync(exercise);
    }

    public async Task<Exercise> GetExerciseAsync(int id)
    {
        return await _exerciseRepo.GetExerciseByIdAsync(id);
    }

    public async Task DeleteExerciseAsync(int id)
    {
        var exercise = await _exerciseRepo.GetExerciseByIdAsync(id);
        if (exercise == null)
        {
            throw new KeyNotFoundException("Meal not fount");

        }
        await _exerciseRepo.RemoveExerciseAsync(exercise);
    }
    #endregion

    #region Routine
    public async Task CreateRoutineAsync(ExerciseRoutine routine)
    {

        await _exerciseRepo.AddRoutineAsync(routine);
    }
    public async Task DeleteFromRoutineAsync(int routineId, int exerciseId)
    {
        await _exerciseRepo.RemoveFromRoutineAsync(routineId,exerciseId);
    }

    public async Task DeleteRoutineAsync(int id)
    {
        //1- check exists
        var routine = await _exerciseRepo.GetRoutineByIdAsync(id);
        if (routine == null)
            throw new KeyNotFoundException("Routine not found");

        var content = await _exerciseRepo.FindAllExercisesInRoutineAsync(id);

        if (content.Count != 0)
            //delete content
            await _exerciseRepo.RemoveExercisesInRoutineAsync(id);


        //3- delete routine
        await _exerciseRepo.RemoveRoutineAsync(routine);
    }

    public async Task AddExerciseToRoutineAsync(int routineId, Exercise exercise)
    {
        var routine = await _exerciseRepo.GetRoutineByIdAsync(routineId);
        if (routine == null)
            throw new KeyNotFoundException("Routine not found");

        if (routine.Exercises == null)
            throw new InvalidOperationException("Routine exercises not loaded");

        if (routine.Exercises.Any(e => e.ExerciseId == exercise.ExerciseId))
            throw new InvalidOperationException("Exercise already exists in routine");
        routine.Exercises.Add(exercise);

        //update routine and save it
        await _exerciseRepo.UpdateRoutineAsync(routine);
    }

    public async Task<List<Exercise>> ListRoutineContentAsync(int routineId)
    {
        var routine = await _exerciseRepo.GetRoutineByIdAsync(routineId);
        if (routine == null)
        {
            throw new KeyNotFoundException("Routine not found");
        }
        var content = await _exerciseRepo.FindAllExercisesInRoutineAsync(routineId);
        return content;
    }

    public async Task<ExerciseRoutine> GetRoutineAsync(int id)
    {
        return await _exerciseRepo.GetRoutineByIdAsync(id);
    }

    public async Task EditRoutineAsync(ExerciseRoutine routine)
    {
        var existingRoutine = await _exerciseRepo.GetRoutineByIdAsync(routine.RoutineId);
        if (existingRoutine == null)
        {
            throw new KeyNotFoundException("Routine not found.");
        }
        await _exerciseRepo.UpdateRoutineAsync(routine);
    }
    #endregion
}