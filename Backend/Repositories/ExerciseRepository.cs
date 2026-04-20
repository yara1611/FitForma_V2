public interface IExerciseRepository
{
    //why task and async? will be added in the notes when im done
    Task<ExerciseRoutine> GetRoutineWithExercisesAsync(int routineId);
    Task AddRoutineAsync(ExerciseRoutine routine);
}

public class ExerciseRepository : IExerciseRepository
{
    public Task AddRoutineAsync(ExerciseRoutine routine)
    {
        throw new NotImplementedException();
    }

    public Task<ExerciseRoutine> GetRoutineWithExercisesAsync(int routineId)
    {
        throw new NotImplementedException();
    }
}