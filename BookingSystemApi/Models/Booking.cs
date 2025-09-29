namespace BookingSystemApi.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public int ActivityId { get; set; }
        public string Description { get; set; }
        public BookingType BookingType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}