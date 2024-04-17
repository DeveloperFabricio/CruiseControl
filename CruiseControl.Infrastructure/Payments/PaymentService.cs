using CruiseControl.Core.PaymentInfo;
using CruiseControl.Core.Services;
using System.Text;
using System.Text.Json;

namespace CruiseControl.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private IMessageService _messageBusService;
        private const string QUEUE_NAME = "Payments";

        public PaymentService(IMessageService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(IPaymentInfo paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
