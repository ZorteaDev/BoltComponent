using BoltComponent.Domain.Entities;
using BoltComponent.Domain.Repositories;
using BoltComponent.Infra;
using BoltComponent.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<BoltComponentContext>(opts => opts.UseNpgsql(connectionString));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/components", async (IBaseRepository<Component> repo) => await repo.GetAll() )
    .WithName("GetComponents")
    .WithOpenApi();

app.MapPost("/components", async (Component entity, IBaseRepository<Component> repo) => await repo.Create(entity) )
    .WithName("CreateComponent")
    .WithOpenApi();

app.MapGet("/components/{id:int}", async (int id, IBaseRepository<Component> repo) => await repo.GetOne(x => x.Id == id) )
    .WithName("GetComponentById")
    .WithOpenApi();

app.MapPatch("/components/{id:int}", async (int id, Component entity, IBaseRepository<Component> repo) => await repo.Update(entity) )
    .WithName("UpdateComponent")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}