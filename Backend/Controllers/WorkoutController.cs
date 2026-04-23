using AutoMapper;
using Microsoft.AspNetCore.Mvc;
/*
ENDPOINTS FOR NOW
GET WORKOUT
POST WORKOUT
DELETE WORKOUT
GET WORKOUT CONTENT
PUT ROUTINE
POST EXERCISE (TO WORKOUT)
DELETE EXERCISE (FROM WORKOUT AND DB)
------------
NOT DONE:
GET ALL WORKOUTS (of user)
PUT EXERCISE
GET Exercise
*/
[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly ExerciseService _service;
    private readonly IMapper _mapper;
    public WorkoutController(ExerciseService service,IMapper mapper)
    {
        _service = service;
        _mapper=mapper;
    }


    //TESTS
    //Exists -> 200
    //NOT -> 204
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoutine(int id)
    {
        var routine = await _service.GetRoutineAsync(id); 
        if(routine==null)
            return NoContent();
        return Ok(routine);
    }
    
    //ROutine not exists -> create -> 200
    //Exists -> duplicate key
    [HttpPost]
    public async Task<IActionResult> CreateRoutine([FromBody] CreateRoutineDto dto, int userId)
    {
        var routine = _mapper.Map<ExerciseRoutine>(dto);
        await _service.CreateRoutineAsync(routine, userId);
        return Ok(routine);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoutine(int id)
    {
        await _service.DeleteRoutineAsync(id);
        return NoContent();
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoutine(int id, [FromBody] UpdateRoutineDto dto)
    {
        
        await _service.EditRoutineAsync(id, dto);
        return Ok(dto);
    }


    //Exercises
    [HttpPost("{routineId}/exercises")]
    public async Task<IActionResult> AddToRoutine(int routineId,[FromBody]CreateExerciseDto dto)
    {
        var exercise = _mapper.Map<Exercise>(dto);
        if (exercise == null)
            return BadRequest("Exercise is required");
        await _service.AddExerciseToRoutineAsync(routineId,exercise);
        return Ok(exercise);

    }

    [HttpDelete("{routineId}/exercises/{exerciseId}")]
    public async Task<IActionResult> DeleteFromRoutine(int routineId, int exerciseId)
    {
        
        await _service.DeleteFromRoutineAsync(routineId,exerciseId);
        
        return NoContent();
    }

    [HttpGet("{routineId}/exercises")]
    public async Task<IActionResult> ListAllExercises(int routineId)
    {
        
        return Ok(await _service.ListRoutineContentAsync(routineId));
    }

    [HttpGet("{routineId}/exercises/{exerciseId}")]
    public async Task<IActionResult> GetExercise(int routineId,int exerciseId)
    {
        
        return Ok(await _service.GetExerciseAsync(exerciseId));
    }
}
