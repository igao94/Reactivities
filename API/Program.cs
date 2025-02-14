using Application.Activities.Queries.GetAllActivities;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddCors();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllActivitiesQuery>());

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:3000", "https://localhost:3000"));

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();

    await context.Database.MigrateAsync();

    await DbInitializer.SeedDataAsync(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();

    logger.LogError(ex, "An error occurred durning migration.");
}

app.Run();
