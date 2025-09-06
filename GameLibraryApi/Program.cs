using GameLibraryApi.Data;
using GameLibraryApi.Interfaces;
using GameLibraryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GamingLibraryContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GameLibraryDB;Trusted_Connection=true;MultipleActiveResultSets=true"));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IGameService, GameDatabaseService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GamingLibraryContext>();
    context.SeedData();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
