using BookingSystemApi.Models;

namespace BookingSystemApi.Services
{
    public class BookingService : IBookingService
    {
        public Task<Booking> AddBookingAsync(CreateBookingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBookingAsync(int bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingAsync(int bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingByCustomerAsync(int cutomerId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Booking>> GetBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> UpdateBookingAsync(int bookingId, UpdateBookingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}