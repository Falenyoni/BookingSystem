using BookingSystemApi.Models;

namespace BookingSystemApi.Services
{
    public interface IBookingService
    {
        Task<Booking> AddBookingAsync(CreateBookingRequest request);

        Task<Booking> GetBookingAsync(int bookingId);

        Task<IReadOnlyList<Booking>> GetBookingsAsync();

        Task<Booking> GetBookingByCustomerAsync(int cutomerId);

        Task<Booking> UpdateBookingAsync(int bookingId, UpdateBookingRequest request);

        Task<bool> DeleteBookingAsync(int bookingId);
    }
}