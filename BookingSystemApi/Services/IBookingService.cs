using BookingSystemApi.Models;

namespace BookingSystemApi.Services
{
    public interface IBookingService
    {
        Task<Booking> AddBookingAsync(CreateBookingRequest request);

        Task<Booking> GetBookingAsync(Guid bookingId);

        Task<IReadOnlyList<Booking>> GetBookingsAsync();

        Task<IReadOnlyList<Booking>> GetBookingByCustomerAsync(Guid cutomerId);

        Task<Booking> UpdateBookingAsync(Guid bookingId, UpdateBookingRequest request);

        Task<bool> DeleteBookingAsync(Guid bookingId);

        Task<Customer> GetCustomerAsync(Guid customerId);

        Task<IReadOnlyList<Customer>> GetCustomersAsync();
    }
}