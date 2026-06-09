using GymMembershipSystem.Services.Models;
namespace GymMembershipSystem.Services.Contracts;
public interface IUserMembershipService
{
    Task<MyMembershipViewModel> MineAsync(string userId);
    Task<ServiceResult> SubscribeAsync(string userId, int planId);
    Task<bool> HasActiveMembershipAsync(string userId);
}
