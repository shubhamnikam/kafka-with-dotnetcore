using Confluent.Kafka;
using Weather.Kafka.Common.Models;

namespace Weather.Kafka.Producer.Services
{
    public interface IWeatherProducerService
    {
        Task<DeliveryResult<Null, string>> PushAsync(WeatherModel weatherModel);
    }
}