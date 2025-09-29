using BookingSystemApi.Models;
using System.Collections.Concurrent;

namespace BookingSystemApi.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ConcurrentDictionary<int, Activity> _activities;

        public ActivityService()
        {
            _activities = new ConcurrentDictionary<int, Activity>();
            SeedDAta();
        }

        public async Task<IReadOnlyList<Activity>> GetActivitiesAsync()
        {
            return _activities.Values.OrderBy(a => a.Name).ToList();
        }

        public async Task<Activity> GetActivityAsync(int activityId)
        {
            if (_activities.TryGetValue(activityId, out var activity))
            {
                return activity;
            }
            throw new KeyNotFoundException($"Activity with ID {activityId} not found");
        }

        private void SeedDAta()
        {
            var seedActivities = new List<Activity>
            {
               new Activity
                {
                    Id = 1,
                    Name = "Luxury Apartment Downtown",
                    Description = "Modern 2-bedroom apartment in city center with stunning views",
                    Category = BookingType.Apartment,
                    Price = 150.00m
                },
               new Activity
                {
                    Id = 2,
                    Name = "Cozy Studio Apartment",
                    Description = "Perfect for solo travelers or couples, fully furnished",
                    Category = BookingType.Apartment,
                    Price = 85.00m
                },
                new Activity
                {
                    Id = 3,
                    Name = "Family Penthouse",
                    Description = "Spacious 3-bedroom penthouse with rooftop terrace",
                    Category = BookingType.Apartment,
                    Price = 350.00m
                },
                new Activity
                {
                    Id = 4,
                    Name = "Economy Sedan",
                    Description = "Fuel-efficient sedan perfect for city driving",
                    Category = BookingType.Vehicle,
                    Price = 35.00m
                },
                new Activity
                {
                    Id = 5,
                    Name = "SUV Adventure",
                    Description = "Spacious SUV ideal for family trips and outdoor adventures",
                    Category = BookingType.Vehicle,
                    Price = 75.00m
                },
                new Activity
                {
                    Id = 6,
                    Name = "Luxury Sports Car",
                    Description = "High-performance sports car for special occasions",
                    Category = BookingType.Vehicle,
                    Price = 200.00m,
                },
                new Activity
                {
                    Id = 7,
                    Name = "Broadway Musical - Hamilton",
                    Description = "Award-winning Broadway musical performance",
                    Category = BookingType.Show,
                    Price = 125.00m,
                },
                new Activity
                {
                    Id = 8,
                    Name = "Comedy Night Stand-up",
                    Description = "Evening of laughs with professional comedians",
                    Category = BookingType.Show,
                    Price = 45.00m
                },
                new Activity
                {
                    Id = 9,
                    Name = "Opera Performance",
                    Description = "Classical opera performance at the grand theater",
                    Category = BookingType.Show,
                    Price = 95.00m
                },
                new Activity
                {
                    Id = 10,
                    Name = "Private Chef Experience",
                    Description = "Personal chef service for special dining experience",
                    Category = BookingType.Other,
                    Price = 300.00m
                },
                 new Activity
                {
                    Id = 11,
                    Name = "Spa Day Package",
                    Description = "Full day relaxation package with massage and treatments",
                    Category = BookingType.Other,
                    Price = 180.00m
                },
                new Activity
                {
                    Id = 12,
                    Name = "City Tour Guide",
                    Description = "Professional guided tour of city landmarks and attractions",
                    Category = BookingType.Other,
                    Price = 65.00m
                }
            };
            foreach (var activity in seedActivities)
            {
                _activities.TryAdd(activity.Id, activity);
            }
        }
    }
}