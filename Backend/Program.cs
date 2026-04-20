var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// OR: options.UseNpgsql() for PostgreSQL not sure yet

//Repos (scoped -> one instance per HTTP request)
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();

//Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ExerciseService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapControllers();



app.Run();

