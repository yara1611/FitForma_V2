using AutoMapper;

public class ExerciseService
{
    private readonly IExerciseRepository _exerciseRepo;
    private readonly IMapper _mapper;
    public ExerciseService(IExerciseRepository exerciseRepo,IMapper mapper)
    {
        _exerciseRepo = exerciseRepo;
        _mapper=mapper;
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
    public async Task CreateRoutineAsync(ExerciseRoutine routine, int userId)
    {
        
        routine.UserId = userId;


        await _exerciseRepo.AddRoutineAsync(routine);
    }
    public async Task DeleteFromRoutineAsync(int routineId, int exerciseId)
    {
        await _exerciseRepo.RemoveFromRoutineAsync(routineId, exerciseId);
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

        // attach exercise to routine
        exercise.RoutineId = routineId;

        await _exerciseRepo.AddExerciseAsync(exercise);
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

    public async Task EditRoutineAsync(int id, UpdateRoutineDto dto)
    {
        var existingRoutine = await _exerciseRepo.GetRoutineByIdAsync(id);
        if (existingRoutine == null)
            throw new KeyNotFoundException("Routine not found.");
        _mapper.Map(dto,existingRoutine);
        existingRoutine.UpdatedAt = DateTime.UtcNow;
        await _exerciseRepo.UpdateRoutineAsync(existingRoutine);
    }
    #endregion
}