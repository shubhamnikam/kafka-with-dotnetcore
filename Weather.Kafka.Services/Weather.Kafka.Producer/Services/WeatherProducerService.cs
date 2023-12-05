using Confluent.Kafka;
using Newtonsoft.Json;
using Weather.Kafka.Common.Configs;
using Weather.Kafka.Common.Models;

namespace Weather.Kafka.Producer.Services
{
    public class WeatherProducerService : IWeatherProducerService
    {
        private readonly IProducer<Null, string> producer;

        public WeatherProducerService(IProducer<Null, string> producer)
        {
            this.producer = producer;
        }

        public async Task<DeliveryResult<Null, string>> PushAsync(WeatherModel weatherModel)
        {
            return await producer.ProduceAsync(AppConstants.WEATHER_KAFKA_TOPIC_NAME,
                new Message<Null, string>()
                {
                    Value = JsonConvert.SerializeObject(weatherModel)
                });
        }
    }
}
