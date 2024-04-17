namespace CruiseControl.Core.PaymentInfo
{
    public interface IPaymentInfo
    {
        int IdClient{ get; }
        string CreditCardNumber { get; }
        string Cvv { get; }
        string ExpiresAt { get; }
        string FullName { get; }
        decimal Amount { get; }
    }
}
