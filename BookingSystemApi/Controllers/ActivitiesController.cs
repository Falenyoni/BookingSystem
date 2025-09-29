using BookingSystemApi.Models;
using BookingSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly ILogger<BookingsController> _logger;

        public ActivitiesController(IActivityService activityService, ILogger<BookingsController> logger)
        {
            _activityService = activityService;
            _logger = logger;
        }

        [HttpGet("activities/{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activity = await _activityService.GetActivityAsync(id);
            return activity;
        }

        [HttpGet("activities")]
        public async Task<ActionResult<IReadOnlyList<Activity>>> GetActivities()
        {
            var activities = await _activityService.GetActivitiesAsync();
            return Ok(activities);
        }
    }
}