using BookingSystemApi.Models;
using BookingSystemApi.Services;
using Moq;

namespace BookingSystemApi.Tests.Services
{
    public class BookingServiceTests
    {
        private readonly Mock<IActivityService> _mockActivityService;
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            _mockActivityService = new Mock<IActivityService>();
            _bookingService = new BookingService(_mockActivityService.Object);
        }

        [Fact]
        public async Task CreateBooking_WithValidRequest_ShouldReturnBooking()
        {
            // Arrange
            var activityId = 1;
            var activity = new Activity
            {
                Id = activityId,
                Name = "Test Activity",
                Description = "Test Description",
                Category = BookingType.Show,
                Price = 100m
            };

            var request = new CreateBookingRequest
            {
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                CustomerPhone = "123-456-7890",
                ActivityId = activityId,
                Description = "Birthday party booking",
                Type = BookingType.Show,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(1).AddHours(2)
            };

            _mockActivityService.Setup(x => x.GetActivityAsync(activityId))
                              .ReturnsAsync(activity);
            // Act
            var result = await _bookingService.AddBookingAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(request.CustomerName, result.Customer.Name);
            Assert.Equal(request.CustomerEmail, result.Customer.Email);
            Assert.Equal(request.CustomerPhone, result.Customer.Phone);
            Assert.Equal(request.ActivityId, result.ActivityId);
        }

        [Fact]
        public async Task GetBooking_WithValidId_ShouldReturnBooking()
        {
            // Arrange
            var activityId = 1;
            var activity = new Activity
            {
                Id = activityId,
                Name = "Test Activity",
                Description = "Test Description",
                Category = BookingType.Show,
                Price = 100m
            };

            var createRequest = new CreateBookingRequest
            {
                CustomerName = "Jane Smith",
                CustomerEmail = "jane.smith@example.com",
                CustomerPhone = "987-654-3210",
                ActivityId = activityId,
                Description = "Anniversary celebration",
                Type = BookingType.Show,
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(2).AddHours(3)
            };

            _mockActivityService.Setup(x => x.GetActivityAsync(activityId))
                              .ReturnsAsync(activity);

            // First create a booking
            var createdBooking = await _bookingService.AddBookingAsync(createRequest);

            // Act
            var result = await _bookingService.GetBookingAsync(createdBooking.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdBooking.Id, result.Id);
            Assert.Equal(createdBooking.Customer.Name, result.Customer.Name);
            Assert.Equal(createdBooking.Customer.Email, result.Customer.Email);
            Assert.Equal(createdBooking.ActivityId, result.ActivityId);
            Assert.NotNull(result.Activity);
            Assert.Equal(activity.Name, result.Activity.Name);
        }
    }
}