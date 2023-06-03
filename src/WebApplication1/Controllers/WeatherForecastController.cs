using System.Buffers.Text;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

public record Model
{
    public required byte[] Data { get; init; }
}

public record Model2
{
    public required string Data { get; init; }
}


public class A( int a)
{
    public void WriteA()
    {
        a = 1;
    }

    public int PrintA() => a;
}

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("1")]
        public int Post(Model model)
        {
            return model.Data.Length;
        }

        [HttpPost("2")]
        public int Post2(Model2 model)
        {
            return Convert.FromBase64String(model.Data).Length;
        }

        [HttpPost("3")]
        public int Post3(Model2 model)
        {
            var a= new A(10);

            a.WriteA();

            Console.WriteLine(a.PrintA());

            var length = model.Data.Length;

            var fileSizeInByte = (length >> 2) * 3;

            if (length >= 2)
            {
                var paddings = model.Data[^2..];
                fileSizeInByte = paddings.Equals("==") ? fileSizeInByte - 2 : paddings[1].Equals('=') ? fileSizeInByte - 1 : fileSizeInByte;
            }

            return fileSizeInByte;
        }
    }
}
