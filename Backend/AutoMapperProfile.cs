using AutoMapper;

public class AutoMapperProfile : Profile
{
    //when mappers get too big switch to feature-based profiles
    public AutoMapperProfile()
    {
        //Workout
        CreateMap<CreateRoutineDto, ExerciseRoutine>().ReverseMap();
        CreateMap<Exercise,ExerciseDto>().ReverseMap();
        CreateMap<UpdateRoutineDto,ExerciseRoutine>()
        .ForAllMembers(opts=>opts.Condition((src,dest,srcMember)=>srcMember!=null));
        CreateMap<ExerciseRoutine, RoutineResponseDto>();

        //Meals
        CreateMap<CreatePlanDto, MealPlan>().ReverseMap();
        CreateMap<Meal,MealDto>().ReverseMap();
        CreateMap<UpdatePlanDto,MealPlan>()
        .ForAllMembers(opts=>opts.Condition((src,dest,srcMember)=>srcMember!=null));
        CreateMap<MealPlan, PlanResponseDto>();
                
        //User
        CreateMap<User,UserResponseDto>().ReverseMap();
        //only update the fields tat are not null in the source object (dto)
        CreateMap<UserProfileDto,UserProfile>()
        .ForAllMembers(opts => opts.Condition((src,dest,srcMember)=>srcMember!=null));
        
    }
}