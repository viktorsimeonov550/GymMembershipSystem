using GymMembershipSystem.Services.Models;
namespace GymMembershipSystem.Services.Contracts;
public interface IStatisticsService
{
    Task<DashboardStatisticsViewModel> GetDashboardStatisticsAsync();
}
