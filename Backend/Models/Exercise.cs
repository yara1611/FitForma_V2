using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Net.Http.Headers;

public class Exercise
{
    [Key]
    public int ExerciseId {set;get;}
    public int RoutineId {set;get;}
    public string Name{get;set;}=null!;
    public string? TargetMuscle {set;get;}
    public int Sets{get;set;}=0;
    public int Reps {get;set;}=0;
    public int RestSeconds{set;get;}=0;

    //Navigation Properties
    public virtual ExerciseRoutine? Routine { get; set; }
}