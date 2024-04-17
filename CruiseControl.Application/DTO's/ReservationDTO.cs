namespace CruiseControl.Application.DTO_s
{
    public class ReservationDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
    }
}
