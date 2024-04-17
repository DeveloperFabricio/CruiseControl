using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace CruiseControl.Application.Asaas.RabbitMQServiceAsaas
{
    public class RabbitMQAsaas
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQAsaas(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQConfig:HostName"],
                UserName = configuration["RabbitMQConfig:UserName"],
                Password = configuration["RabbitMQConfig:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            var exchangeName = configuration["RabbitMQConfig:ExchangeName"];
            var queueName = configuration["RabbitMQConfig:QueueNames"];

            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            var exchangeName = _configuration["RabbitMQConfig:ExchangeName"];
            var queueName = _configuration["RabbitMQConfig:QueueNames"];

            _channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);
        }
    }
}
