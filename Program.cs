using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories.Contracts;
using TodoApi.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["DbContextSettings:ConnectionString"];

builder.Services.AddDbContext<TodoContext>(opt =>
  opt.UseNpgsql(connectionString)
);

builder.Services.AddScoped<ITodoRepository, TodoItemsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  // app.UseSwagger();
  // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
