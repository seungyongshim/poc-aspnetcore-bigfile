using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();