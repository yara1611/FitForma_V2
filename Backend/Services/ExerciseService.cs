public class ExerciseService
{
    private static List<ExerciseRoutine> routines = new List<ExerciseRoutine>();
    public void CreateExercise(){}
    public void CreateRoutine()
    {
        ExerciseRoutine exerciseRoutine = new ExerciseRoutine
        {
            RoutineId=1,
            Name="New Routine Test",
            UserId=1
        };
        routines.Add(exerciseRoutine);
    }
}