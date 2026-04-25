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
    
    //ROutine not exists -> create -> 200
    //Exists -> duplicate key
    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanDto dto, int userId)
    {
        var plan = _mapper.Map<MealPlan>(dto);
        await _service.CreatePlanAsync(plan, userId);
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
        return Ok(dto);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetMealPlansByUserIdAsync(int userId)
    {
        return Ok(await _service.GetAllPlansByUserId(userId));
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
}
