using CruiseControl.Core.Services;
using RabbitMQ.Client;

namespace CruiseControl.Infrastructure.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly ConnectionFactory _factory;

        public MessageService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
            };
        }
        public void Publish(string queue, byte[] message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: message);
                }
            }
        }
    }
}
