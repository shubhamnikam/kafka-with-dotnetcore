using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Kafka.Common.Configs
{
    public class AppConstants
    {
        public const string WEATHER_KAFKA_CONSUMER_GROUP_NAME = "weather-consumer-group";
        public const string WEATHER_KAFKA_TOPIC_NAME = "weather-city-topic";
        public const string WEATHER_KAFKA_SERVER_URL = "localhost:9092";
    }
}
