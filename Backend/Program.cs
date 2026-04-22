using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//builder.Services.AddAuthentication();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// OR: options.UseSqlServer() for PostgreSQL not sure yet

//Repos (scoped -> one instance per HTTP request)
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();

//Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<MealService>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

//app.UseHttpsRedirection();
// app.UseAuthentication();
// app.UseAuthorization();
app.MapControllers();



app.Run();

