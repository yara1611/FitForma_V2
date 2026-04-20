using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Net.Http.Headers;

public class Exercise
{
    [Key]
    public int ExerciseId {set;get;}
    [ForeignKey("RoutineId")]
    public int RoutineId {set;get;}
    public string Name{get;set;}
    public string TargetMuscle {set;get;}
    public int sets{get;set;}
    public int reps {get;set;}
    public int RestSeconds{set;get;}

    //Navigation Properties
    public virtual ExerciseRoutine Routine { get; set; }
}