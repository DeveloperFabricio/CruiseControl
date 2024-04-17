namespace CruiseControl.Application.DTO_s
{
    public class PaymentDTO
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
}
