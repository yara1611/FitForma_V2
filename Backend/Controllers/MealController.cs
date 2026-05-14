using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
/*
ENDPOINTS FOR NOW
GET PLAN
POST PLAN
DELETE PLAN
GET PLAN CONTENT
PUT PLAN
POST MEAL (TO PLAN)
DELETE MEAL (FROM PLAN AND DB)
GET MEAL
GET ALL PLANS (of user)
------------
NOT DONE:
PUT MEAL -> Skipped for now, can be done in the future if needed. For now, if user wants to edit meal details, they can just delete and re-add the meal with updated details.

*/

//[Authorize]
[ApiController]
[Route("[controller]")]
public class MealPlanController : ControllerBase
{
    //make the services have interfaces
    private readonly IMealService _service;
    private readonly IMapper _mapper;
    private readonly MealGeneratorService _generatorService;
    public MealPlanController(IMealService service,IMapper mapper,MealGeneratorService generatorService)
    {
        _service = service;
        _mapper=mapper;
        _generatorService=generatorService;
    }


    //TESTS
    //Exists -> 200
    //NOT -> 204
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlan(int id)
    {
        var plan = await _service.GetPlanAsync(id); 
        if(plan==null)
            return NotFound();
       
        return Ok(_mapper.Map<PlanResponseDto>(plan));
    }
    
    //Routine not exists -> create -> 200
    //Exists -> duplicate key
    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized("User ID not found in token.");
        }
        var plan = _mapper.Map<MealPlan>(dto);
        await _service.CreatePlanAsync(plan, int.Parse(userId));
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlan(int id)
    {
        await _service.DeletePlanAsync(id);
        return NoContent();
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlan(int id, [FromBody] UpdatePlanDto dto)
    {
        
        await _service.EditPlanAsync(id, dto);
        return NoContent();
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetMealPlansByUserIdAsync()
    {
        //get current user id from token
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized("User ID not found in token.");
        }
        var plans = await _service.GetAllPlansByUserId(int.Parse(userId));
        
        return Ok(_mapper.Map<List<PlanResponseDto>>(plans));
    }

    //Meals
    [HttpPost("{planId}/meals")]
    public async Task<IActionResult> AddToPlan(int planId,[FromBody]MealDto dto)
    {
        var meal = _mapper.Map<Meal>(dto);
        if (meal == null)
            return BadRequest("Meal is required");
        await _service.AddMealToPlanAsync(planId,meal);
        return Ok(meal);

    }

    [HttpDelete("{planId}/meals/{mealId}")]
    public async Task<IActionResult> DeleteFromPlan(int planId, int mealId)
    {
        
        await _service.DeleteMealAsync(mealId);
        
        return NoContent();
    }

    [HttpGet("{planId}/meals")]
    public async Task<IActionResult> ListAllMeals(int planId)
    {
        
        return Ok(await _service.ListPlanContentAsync(planId));
    }


    [HttpGet("{planId}/meals/{mealId}")]
    public async Task<IActionResult> GetMeal(int planId,int mealId)
    {
        
        return Ok(await _service.GetMealAsync(mealId));
    }

    [HttpGet("generate")]
    public async Task<IActionResult> GenerateMealPlan()
    {
        _generatorService.Generate();
        return Ok("Not implemented yet");
    }

    
}
