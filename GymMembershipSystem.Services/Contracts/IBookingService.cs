using GymMembershipSystem.Services.Models;
namespace GymMembershipSystem.Services.Contracts;
public interface IBookingService
{
    Task<IEnumerable<BookingViewModel>> MineAsync(string userId);
    Task<ServiceResult> BookAsync(string userId, int workoutClassId);
    Task<ServiceResult> CancelAsync(string userId, int bookingId);
}
