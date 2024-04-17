using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CruiseControl.Application.GoogleCalendar.RabbitMQServiceCalendar
{
    public class RabbitMQService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly GoogleCalendarService _googleCalendarService;

        public RabbitMQService(IConfiguration configuration, GoogleCalendarService googleCalendarService)
        {
            _configuration = configuration;
            _googleCalendarService = googleCalendarService;

            var rabbitMQConfig = _configuration.GetSection("RabbitMQConfig");

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig["HostName"],
                UserName = rabbitMQConfig["UserName"],
                Password = rabbitMQConfig["Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            var exchangeName = rabbitMQConfig["ExchangeName"];
            var queueName = rabbitMQConfig["QueueName"];

            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var messageBytes = eventArgs.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(messageBytes);

                try
                {
                    var calendarEvent = JsonSerializer.Deserialize<Event>(messageJson);

                    var events = await _googleCalendarService.GetEventsOnDateAsync(calendarEvent.Start.DateTime ?? DateTime.Now, stoppingToken);

                    foreach (var evt in events)
                    {
                        PostMessage(evt.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing calendar event: {ex.Message}");
                }
                finally
                {
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
            };

            var queueName = _configuration["RabbitMQConfig:QueueName"];
            _channel.BasicConsume(queueName, false, consumer);

            var queueNames = "GoogleCalendarEvents";
            _channel.BasicConsume(queueName, false, consumer);


            await Task.CompletedTask;

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return base.StopAsync(cancellationToken);
        }

        public void PostMessage(string mensagem)
        {
            var body = Encoding.UTF8.GetBytes(mensagem);

            var exchangeName = _configuration["RabbitMQConfig:ExchangeName"];
            var queueName = _configuration["RabbitMQConfig:QueueName"];

            _channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);
        }
    }
}
