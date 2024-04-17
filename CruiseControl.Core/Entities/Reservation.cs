namespace CruiseControl.Core.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Customer Customer { get; set; }
    }
}
