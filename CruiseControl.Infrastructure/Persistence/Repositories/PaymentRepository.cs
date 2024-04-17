using CruiseControl.Core.Entities;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _appDbContext;

        public PaymentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _appDbContext.Payments.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _appDbContext.Payments.ToListAsync();
        }

        public async Task AddPayment(Payment payment)
        {
            _appDbContext.Payments.Add(payment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdatePayment(int id, Payment updatedPayment)
        {
            var existingPayment = await _appDbContext.Payments.FindAsync(id);
            if (existingPayment == null)
            {
                throw new NotFoundException($"Payment with id {id} not found");
            }

            existingPayment.Amount = updatedPayment.Amount;
            existingPayment.PaymentMethod = updatedPayment.PaymentMethod;
           

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                {
                    throw new NotFoundException($"Payment with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _appDbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                throw new NotFoundException($"Payment with id {id} not found");
            }

            _appDbContext.Payments.Remove(payment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        private bool PaymentExists(int id)
        {
            return _appDbContext.Payments.Any(e => e.Id == id);
        }

        public async Task Start(Payment payment)
        {
            payment.Start();
            _appDbContext.Payments.Update(payment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Finish(Payment payment)
        {
            payment.Finish();
            _appDbContext.Payments.Update(payment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Cancel(Payment payment)
        {
            payment.Cancel();
            _appDbContext.Payments.Update(payment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Update(Payment payment)
        {
            _appDbContext.Payments.Update(payment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task SetPaymentPending(Payment payment)
        {
            payment.SetPaymentPending();
            _appDbContext.Payments.Update(payment);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
