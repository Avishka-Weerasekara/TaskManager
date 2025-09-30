using Microsoft.OpenApi.Models;
using TaskManager.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddSingleton<TaskService>();
builder.Services.AddControllers();
builder.Services.AddRazorPages(); // Add this line

// Add Swagger for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskManager API",
        Version = "v1",
        Description = "A simple API for managing tasks"
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager API V1");
    c.RoutePrefix = string.Empty; // Swagger at root URL
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages(); // Enable Razor Pages

app.Run();
