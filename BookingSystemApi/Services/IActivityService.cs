using BookingSystemApi.Models;

namespace BookingSystemApi.Services
{
    public interface IActivityService
    {
        Task<Activity> GetActivityAsync(int activityId);

        Task<IReadOnlyList<Activity>> GetActivitiesAsync();
    }
}