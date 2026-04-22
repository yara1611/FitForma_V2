using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ExerciseRoutine
{
    [Key]
    public int RoutineId {set;get;}
    public int UserId {set;get;} //one to many (user can have many routines)
    public string Name {set;get;}="New Routine";
    public string? Description {set;get;}
    public DateTime CreatedAt {set;get;} = DateTime.UtcNow;
    public DateTime UpdatedAt {set;get;} = DateTime.UtcNow;

    //Navigation Properties
    public virtual User User { get; set; }=null!;
    public virtual List<Exercise> Exercises { get; set; } = new();
}