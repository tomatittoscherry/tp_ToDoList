using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using tp_ToDoList.Repository;
using tp_ToDoList.Services;
using tp_ToDoList.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();


builder.Services.AddMvc().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


string connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ToDoContext>(config =>
{
    config.UseSqlServer(connectionString);
});


builder.Services.AddScoped<ITareasService, TareasService>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
