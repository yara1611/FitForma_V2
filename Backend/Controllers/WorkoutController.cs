using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly ExerciseService _service;
    public WorkoutController(ExerciseService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoutine(int id)
    {
        return Ok(await _service.GetRoutineAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateRoutine([FromBody] CreateRoutineDto dto, int userId)
    {
        var routine = new ExerciseRoutine
        {
            Name = dto.Name,
            Description = dto.Description
        };
        await _service.CreateRoutineAsync(routine, userId);
        return Ok(routine);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoutine(int id)
    {
        await _service.DeleteRoutineAsync(id);
        return NoContent();
    }

    //edit is tricky for me
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoutine(int id, [FromBody] ExerciseRoutine routine)
    {
        await _service.EditRoutineAsync(id, routine);
        return Ok(routine);
    }


    //Exercises
    [HttpPost("{routineId}/exercises")]
    public async Task<IActionResult> AddToRoutine(int routineId,[FromBody]CreateExerciseDto dto)
    {
        var exercise = new Exercise
    {
        Name = dto.Name,
        TargetMuscle = dto.TargetMuscle,
        Sets = dto.Sets,
        Reps = dto.Reps,
        RestSeconds = dto.RestSeconds,
        RoutineId = routineId
    };
        if (exercise == null)
            return BadRequest("Exercise is required");
        await _service.AddExerciseToRoutineAsync(routineId,exercise);
        return Ok(exercise);

    }

    [HttpDelete("{routineId}/exercises/{exerciseId}")]
    public async Task<IActionResult> DeleteFromRoutine(int routineId, int exerciseId)
    {
        //check if not empty in service
        await _service.DeleteFromRoutineAsync(routineId,exerciseId);
        
        return NoContent();
    }

    [HttpGet("{routineId}/exercises")]
    public async Task<IActionResult> ListAllExercises(int routineId)
    {
        
        return Ok(await _service.ListRoutineContentAsync(routineId));
    }

}
