using CruiseControl.Core.Entities;

namespace CruiseControl.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentById(int id);
        Task<IEnumerable<Payment>> GetAllPayments();
        Task AddPayment(Payment payment);
        Task UpdatePayment(int id, Payment payment);
        Task DeletePayment(int id);
        Task Start(Payment payment);
        Task Finish(Payment payment);
        Task Cancel(Payment payment);
        Task Update(Payment payment);
        Task SetPaymentPending(Payment payment);
        Task SaveChangesAsync();
    }
}
