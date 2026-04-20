using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ExerciseRoutine
{
    [Key]
    public int RoutineId {set;get;}

    [ForeignKey("UserId")]
    public int UserId {set;get;} //one to many (user can have many routines)
    public string Name {set;get;}
    public string Description {set;get;}
    public DateTime CreatedAt {set;get;}

    //Navigation Properties
    public virtual User User { get; set; }
    public virtual List<Exercise> Exercises { get; set; } = new();
}