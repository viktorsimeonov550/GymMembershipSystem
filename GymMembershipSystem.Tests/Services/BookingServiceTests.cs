using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Implementations;
using GymMembershipSystem.Tests.Helpers;
using NUnit.Framework;

namespace GymMembershipSystem.Tests.Services;

[TestFixture]
public class BookingServiceTests
{
    [Test]
    public async Task BookAsyncDoesNotAllowBookingWithoutActiveMembership()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var workout = await AddWorkoutAsync(context, capacity: 10);
        var membershipService = new UserMembershipService(context);
        var bookingService = new BookingService(context, membershipService);

        var result = await bookingService.BookAsync("user-1", workout.Id);

        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.Message, Does.Contain("active membership"));
    }

    [Test]
    public async Task BookAsyncDoesNotAllowDuplicateBooking()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var workout = await AddWorkoutAsync(context, capacity: 10);
        await AddActiveMembershipAsync(context, "user-1");
        context.Bookings.Add(new Booking { UserId = "user-1", WorkoutClassId = workout.Id, BookedOn = DateTime.UtcNow, IsCancelled = false });
        await context.SaveChangesAsync();
        var membershipService = new UserMembershipService(context);
        var bookingService = new BookingService(context, membershipService);

        var result = await bookingService.BookAsync("user-1", workout.Id);

        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.Message, Does.Contain("already booked"));
    }

    [Test]
    public async Task BookAsyncDoesNotAllowBookingFullWorkoutClass()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var workout = await AddWorkoutAsync(context, capacity: 1);
        await AddActiveMembershipAsync(context, "user-1");
        context.Bookings.Add(new Booking { UserId = "other-user", WorkoutClassId = workout.Id, BookedOn = DateTime.UtcNow, IsCancelled = false });
        await context.SaveChangesAsync();
        var membershipService = new UserMembershipService(context);
        var bookingService = new BookingService(context, membershipService);

        var result = await bookingService.BookAsync("user-1", workout.Id);

        Assert.That(result.Succeeded, Is.False);
        Assert.That(result.Message, Does.Contain("full"));
    }

    [Test]
    public async Task CancelAsyncCancelsExistingBooking()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var workout = await AddWorkoutAsync(context, capacity: 10);
        var booking = new Booking { UserId = "user-1", WorkoutClassId = workout.Id, BookedOn = DateTime.UtcNow, IsCancelled = false };
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();
        var membershipService = new UserMembershipService(context);
        var bookingService = new BookingService(context, membershipService);

        var result = await bookingService.CancelAsync("user-1", booking.Id);

        Assert.That(result.Succeeded, Is.True);
        Assert.That(booking.IsCancelled, Is.True);
    }

    private static async Task<WorkoutClass> AddWorkoutAsync(GymMembershipSystem.Data.ApplicationDbContext context, int capacity)
    {
        var trainer = new Trainer { FullName = "Ivan Petrov", Specialization = "Fitness", Biography = "Fitness trainer biography.", ExperienceYears = 5, ImageUrl = "/trainer.jpg" };
        var location = new GymLocation { Name = "Gym Sofia", City = "Sofia", Address = "Center", ImageUrl = "/location.jpg" };
        context.Trainers.Add(trainer);
        context.GymLocations.Add(location);
        await context.SaveChangesAsync();
        var workout = new WorkoutClass { Title = "Morning Cardio", Description = "Cardio class description.", StartTime = DateTime.Now.AddDays(1), DurationMinutes = 45, Capacity = capacity, ImageUrl = "/cardio.jpg", TrainerId = trainer.Id, GymLocationId = location.Id, IsDeleted = false };
        context.WorkoutClasses.Add(workout);
        await context.SaveChangesAsync();
        return workout;
    }

    private static async Task AddActiveMembershipAsync(GymMembershipSystem.Data.ApplicationDbContext context, string userId)
    {
        var plan = new MembershipPlan { Name = "Premium", Description = "Premium membership plan", Price = 90, DurationInDays = 30, ImageUrl = "/premium.jpg", IsDeleted = false };
        context.MembershipPlans.Add(plan);
        await context.SaveChangesAsync();
        context.UserMemberships.Add(new UserMembership { UserId = userId, MembershipPlanId = plan.Id, StartDate = DateTime.UtcNow.AddDays(-1), EndDate = DateTime.UtcNow.AddDays(20), IsActive = true });
        await context.SaveChangesAsync();
    }
}
