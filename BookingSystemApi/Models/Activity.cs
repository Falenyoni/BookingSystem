namespace BookingSystemApi.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BookingType Category { get; set; }
        public decimal Price { get; set; }
    }
}