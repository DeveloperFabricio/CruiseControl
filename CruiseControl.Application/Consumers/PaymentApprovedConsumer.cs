﻿using CruiseControl.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using CruiseControl.Core.IntegrationEvents;
using System.Text.Json;

namespace CruiseControl.Application.Consumers
{
    public class PaymentApprovedConsumer : BackgroundService
    {
        private const string PAYMENT_APPROVED_QUEUE = "PaymentsApproved";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public PaymentApprovedConsumer(IServiceProvider servicesProvider)
        {
            _serviceProvider = servicesProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: PAYMENT_APPROVED_QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var paymentApprovedBytes = eventArgs.Body.ToArray();
                var paymentApprovedJson = Encoding.UTF8.GetString(paymentApprovedBytes);

                var paymentApprovedIntegrationEvent = JsonSerializer.Deserialize<PaymentApprovedIntegrationEvent>(paymentApprovedJson);

                await FinishPayment(paymentApprovedIntegrationEvent.IdPayment);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(PAYMENT_APPROVED_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public async Task FinishPayment(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var paymentRepository = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();

                var finaly = await paymentRepository.GetPaymentById(id);

                finaly.Finish();

                await paymentRepository.SaveChangesAsync();
            }
        }
    }
}
