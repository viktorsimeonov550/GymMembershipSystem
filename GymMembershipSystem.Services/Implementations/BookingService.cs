using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.EntityFrameworkCore;
namespace GymMembershipSystem.Services.Implementations;
public class BookingService : IBookingService
{
    private readonly ApplicationDbContext context;
    private readonly IUserMembershipService memberships;
    public BookingService(ApplicationDbContext context, IUserMembershipService memberships) { this.context = context; this.memberships = memberships; }
    public async Task<IEnumerable<BookingViewModel>> MineAsync(string userId) => await context.Bookings.Where(b => b.UserId == userId).AsNoTracking().Select(b => new BookingViewModel { Id = b.Id, WorkoutTitle = b.WorkoutClass.Title, TrainerName = b.WorkoutClass.Trainer.FullName, StartTime = b.WorkoutClass.StartTime, IsCancelled = b.IsCancelled }).ToListAsync();
    public async Task<ServiceResult> BookAsync(string userId, int workoutClassId)
    {
        if (!await memberships.HasActiveMembershipAsync(userId)) return ServiceResult.Failure("You need an active membership to book a workout.");
        var workout = await context.WorkoutClasses.Include(w => w.Bookings).FirstOrDefaultAsync(w => w.Id == workoutClassId && !w.IsDeleted);
        if (workout == null) return ServiceResult.Failure("Workout class was not found.");
        if (workout.StartTime <= DateTime.Now) return ServiceResult.Failure("You cannot book a workout in the past.");
        if (workout.Bookings.Any(b => b.UserId == userId && !b.IsCancelled)) return ServiceResult.Failure("You already booked this workout.");
        if (workout.Bookings.Count(b => !b.IsCancelled) >= workout.Capacity) return ServiceResult.Failure("This workout class is full.");
        context.Bookings.Add(new Booking { UserId = userId, WorkoutClassId = workoutClassId, BookedOn = DateTime.UtcNow, IsCancelled = false });
        await context.SaveChangesAsync(); return ServiceResult.Success("Workout booked successfully.");
    }
    public async Task<ServiceResult> CancelAsync(string userId, int bookingId)
    {
        var booking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId && !b.IsCancelled);
        if (booking == null) return ServiceResult.Failure("Booking was not found.");
        booking.IsCancelled = true; await context.SaveChangesAsync(); return ServiceResult.Success("Booking cancelled successfully.");
    }
}
