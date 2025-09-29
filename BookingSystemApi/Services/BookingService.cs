using BookingSystemApi.Models;
using System.Collections.Concurrent;

namespace BookingSystemApi.Services;

public class BookingService : IBookingService
{
    private readonly ConcurrentDictionary<Guid, Booking> _bookings;
    private readonly ConcurrentDictionary<Guid, Customer> _customers;
    private readonly IActivityService _activityService;

    public BookingService(IActivityService activityService)
    {
        _bookings = new ConcurrentDictionary<Guid, Booking>();
        _customers = new ConcurrentDictionary<Guid, Customer>();
        _activityService = activityService;
    }

    public async Task<Booking> AddBookingAsync(CreateBookingRequest request)
    {
        // Validation
        ValidateBookingRequest(request);

        var activity = await _activityService.GetActivityAsync(request.ActivityId);

        // Create Customer / Get custommer
        var customer = await GetOrCreateCustomerAsync(request.CustomerName, request.CustomerEmail, request.CustomerPhone);

        // Create Booking
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            Customer = customer,
            BookingType = request.Type,
            ActivityId = request.ActivityId,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedAt = DateTime.UtcNow,
        };

        _bookings.TryAdd(booking.Id, booking);
        return booking;
    }

    public async Task<bool> DeleteBookingAsync(Guid bookingId)
    {
        return _bookings.TryRemove(bookingId, out _);
    }

    public async Task<Booking> GetBookingAsync(Guid bookingId)
    {
        if (_bookings.TryGetValue(bookingId, out var booking))
        {
            // Load activity if not already loaded
            if (booking.Activity == null && booking.ActivityId > 0)
            {
                try
                {
                    booking.Activity = await _activityService.GetActivityAsync(booking.ActivityId);
                }
                catch (KeyNotFoundException)
                {
                    // Activity might have been deleted, handle gracefully
                }
            }
            return booking;
        }
        throw new Exception($"Booking with ID {bookingId} not found");
    }

    public async Task<IReadOnlyList<Booking>> GetBookingByCustomerAsync(Guid cutomerId)
    {
        return _bookings.Values.Where(b => b.Customer.Id == cutomerId).ToList();
    }

    public async Task<IReadOnlyList<Booking>> GetBookingsAsync()
    {
        return _bookings.Values.ToList();
    }

    public Task<Booking> UpdateBookingAsync(Guid bookingId, UpdateBookingRequest request)
    {
        throw new NotImplementedException();
    }

    private async Task<Customer> GetOrCreateCustomerAsync(string name, string email, string phone)
    {
        // check  if customer already exists by email
        var exixtingCustomer = _customers.Values.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (exixtingCustomer != null)
        {
            // update customer if provided details are different
            if (!string.IsNullOrEmpty(name) && exixtingCustomer.Name != name)
                exixtingCustomer.Name = name;
            if (!string.IsNullOrEmpty(phone) && exixtingCustomer.Phone != phone)
                exixtingCustomer.Phone = phone;

            return exixtingCustomer;
        }

        // Create new customer
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Phone = phone,
        };

        // Thread-safe add operation
        _customers.TryAdd(customer.Id, customer);
        return customer;
    }

    private void ValidateBookingRequest(CreateBookingRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.CustomerName))
            errors.Add("Customer name is required");
        if (string.IsNullOrWhiteSpace(request.CustomerEmail))
            errors.Add("Customer email is required");
        if (request.ActivityId <= 0)
            errors.Add("Activity ID must be a positive integer");
        if (request.StartDate <= DateTime.UtcNow)
            errors.Add("Start date must be in the future");
        if (request.EndDate <= request.StartDate)
            errors.Add("End date must be after start date");
        if (string.IsNullOrWhiteSpace(request.CustomerName))
            errors.Add("customer name is required");
    }
}