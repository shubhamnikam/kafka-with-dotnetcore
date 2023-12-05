using Confluent.Kafka;
using Newtonsoft.Json;
using System.Reflection;
using Weather.Kafka.Common.Configs;
using Weather.Kafka.Common.Models;

namespace Weather.Kafka.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Name} started");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Name} stopped");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var kafkaConsumerConfig = new ConsumerConfig()
            {
                GroupId = AppConstants.WEATHER_KAFKA_CONSUMER_GROUP_NAME,
                BootstrapServers = AppConstants.WEATHER_KAFKA_SERVER_URL,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumerBuilder = new ConsumerBuilder<Null, string>(kafkaConsumerConfig).Build())
            {
                consumerBuilder.Subscribe(AppConstants.WEATHER_KAFKA_TOPIC_NAME);
                CancellationTokenSource cancellationTokenSource = new();

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var response = consumerBuilder.Consume(cancellationTokenSource.Token);
                        if (response is not null)
                        {
                            var result = response.Message.Value;
                            _logger.LogInformation($"{DateTimeOffset.UtcNow} {result}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{DateTimeOffset.UtcNow} {ex.Message}");
                    }
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}