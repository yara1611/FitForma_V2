using AutoMapper;

public class AutoMapperProfile : Profile
{
    //when mappers get too big switch to feature-based profiles
    public AutoMapperProfile()
    {
        //Workout
        CreateMap<CreateRoutineDto, ExerciseRoutine>().ReverseMap();
        CreateMap<Exercise,CreateExerciseDto>().ReverseMap();
        CreateMap<UpdateRoutineDto,ExerciseRoutine>()
        .ForAllMembers(opts=>opts.Condition((src,dest,srcMember)=>srcMember!=null));

        //Meals
         CreateMap<CreatePlanDto, MealPlan>().ReverseMap();
        CreateMap<Meal,CreateMealDto>().ReverseMap();
        CreateMap<UpdatePlanDto,MealPlan>()
        .ForAllMembers(opts=>opts.Condition((src,dest,srcMember)=>srcMember!=null));

        //User
        CreateMap<User,UserResponseDto>().ReverseMap();

        //only update the fields tat are not null in the source object (dto)
        CreateMap<UserProfileDto,UserProfile>()
        .ForAllMembers(opts => opts.Condition((src,dest,srcMember)=>srcMember!=null));
        
    }
}