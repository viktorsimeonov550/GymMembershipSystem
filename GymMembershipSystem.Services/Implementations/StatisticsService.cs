using GymMembershipSystem.Data;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.EntityFrameworkCore;
namespace GymMembershipSystem.Services.Implementations;
public class StatisticsService : IStatisticsService
{
    private readonly ApplicationDbContext context;
    public StatisticsService(ApplicationDbContext context) => this.context = context;
    public async Task<DashboardStatisticsViewModel> GetDashboardStatisticsAsync() => new()
    {
        TotalPlans = await context.MembershipPlans.CountAsync(p => !p.IsDeleted),
        TotalTrainers = await context.Trainers.CountAsync(t => !t.IsDeleted),
        TotalWorkoutClasses = await context.WorkoutClasses.CountAsync(w => !w.IsDeleted),
        TotalBookings = await context.Bookings.CountAsync(),
        ActiveMemberships = await context.UserMemberships.CountAsync(m => m.IsActive && m.EndDate >= DateTime.UtcNow)
    };
}
