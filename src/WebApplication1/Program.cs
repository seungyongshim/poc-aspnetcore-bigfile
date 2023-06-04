using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IValidator<Model>, ModelValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    var watch = new Stopwatch();
    watch.Start();
    await next(context);
    watch.Stop();
    var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
    Console.WriteLine($"X-Response-Time-ms {responseTimeForCompleteRequest}");
});

app.UseAuthorization();

app.MapControllers();

app.MapPost("WeatherForecast/0", (Model model) => model.Data.Length)
   .AddEndpointFilter<ValidationFilter<Model>>();

app.Run();
