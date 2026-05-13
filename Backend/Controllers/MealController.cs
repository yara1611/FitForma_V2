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
PUT MEAL

*/
[ApiController]
[Route("[controller]")]
public class MealPlanController : ControllerBase
{
    //make the services have interfaces
    private readonly IMealService _service;
    private readonly IMapper _mapper;
    public MealPlanController(IMealService service,IMapper mapper)
    {
        _service = service;
        _mapper=mapper;
    }


    //TESTS
    //Exists -> 200
    //NOT -> 204
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlan(int id)
    {
        var plan = await _service.GetPlanAsync(id); 
        if(plan==null)
            return NoContent();
        return Ok(plan);
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
        return Ok(plan);
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
        return Ok(await _service.GetAllPlansByUserId(int.Parse(userId)));
    }

    //Meals
    [HttpPost("{planId}/meals")]
    public async Task<IActionResult> AddToPlan(int planId,[FromBody]CreateMealDto dto)
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

    //PUT meal -> update meal details (name, calories, etc) -> mealId in body or url?
}
