using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Weather.Kafka.Common.Models;
using Weather.Kafka.Producer.Services;

namespace Weather.Kafka.Producer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherProducerController : ControllerBase
    {
        private readonly ILogger<WeatherProducerController> _logger;
        private readonly IWeatherProducerService weatherProducerService;

        public WeatherProducerController(ILogger<WeatherProducerController> logger, IWeatherProducerService weatherProducerService)
        {
            _logger = logger;
            this.weatherProducerService = weatherProducerService;
        }

        [HttpPost(Name = "Push")]
        public async Task<IActionResult> PushAsync([FromBody] WeatherModel weatherModel)
        {
            try
            {
                weatherModel.Id = Guid.NewGuid().ToString();
                weatherModel.CreatedTimestamp = DateTimeOffset.UtcNow;
                var result = await weatherProducerService.PushAsync(weatherModel);
                return Ok(new { status = true, message = "message pushed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { status = false, message = ex.Message }));
            }
        }
    }
}