namespace BookingSystemApi.Models
{
    public class UpdateBookingRequest
    {
        public int? ActivityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}