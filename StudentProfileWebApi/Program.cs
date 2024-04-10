using Microsoft.EntityFrameworkCore;
using StudentProfileWebApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("https://localhost:5002/");
builder.Services.AddControllers();
builder.Services.AddDbContext<StudentProfileContext>(x => x.UseInMemoryDatabase("StudentProfiles"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins(
    "https://localhost:5001" // allow requests from the MVC client
    );
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
