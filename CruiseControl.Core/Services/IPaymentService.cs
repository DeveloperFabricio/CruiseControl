using CruiseControl.Core.PaymentInfo;

namespace CruiseControl.Core.Services
{
    public interface IPaymentService
    {
        void ProcessPayment(IPaymentInfo paymentInfo);
    }
}
