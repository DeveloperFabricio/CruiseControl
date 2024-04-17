using CruiseControl.Core.Enums;

namespace CruiseControl.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Date { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public PaymentsStatusEnum Status { get; private set; }
       
        public Payment()
        {
            CreatedAt = DateTime.Now;
            Status = PaymentsStatusEnum.Created;
        }

        public void Cancel()
        {
            if (Status == PaymentsStatusEnum.InProgress || Status == PaymentsStatusEnum.Created)
            {
                Status = PaymentsStatusEnum.Cancelled;
            }
        }

        public void Start()
        {
            if (Status == PaymentsStatusEnum.Created)
            {
                Status = PaymentsStatusEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Finish()
        {
            if (Status == PaymentsStatusEnum.PaymentPending)
            {
                Status = PaymentsStatusEnum.Finished;
                FinishedAt = DateTime.Now;
            }
        }

        public void SetPaymentPending()
        {
            Status = PaymentsStatusEnum.PaymentPending;
            FinishedAt = null;
        }
    }
}
