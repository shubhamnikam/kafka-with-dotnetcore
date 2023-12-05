using Confluent.Kafka;
using Weather.Kafka.Common.Configs;
using Weather.Kafka.Producer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
var kafkaProducerConfig = new ProducerConfig() { BootstrapServers = AppConstants.WEATHER_KAFKA_SERVER_URL};
builder.Services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(kafkaProducerConfig).Build());
builder.Services.AddScoped<IWeatherProducerService, WeatherProducerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
