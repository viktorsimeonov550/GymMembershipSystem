using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.EntityFrameworkCore;
namespace GymMembershipSystem.Services.Implementations;
public class UserMembershipService : IUserMembershipService
{
    private readonly ApplicationDbContext context;
    public UserMembershipService(ApplicationDbContext context) => this.context = context;
    public async Task<MyMembershipViewModel> MineAsync(string userId)
    {
        var membership = await context.UserMemberships.Include(m => m.MembershipPlan).Where(m => m.UserId == userId && m.IsActive && m.EndDate >= DateTime.UtcNow).AsNoTracking().FirstOrDefaultAsync();
        if (membership == null) return new MyMembershipViewModel();
        return new MyMembershipViewModel { PlanName = membership.MembershipPlan.Name, StartDate = membership.StartDate, EndDate = membership.EndDate, IsActive = true };
    }
    public async Task<bool> HasActiveMembershipAsync(string userId) => await context.UserMemberships.AnyAsync(m => m.UserId == userId && m.IsActive && m.EndDate >= DateTime.UtcNow);
    public async Task<ServiceResult> SubscribeAsync(string userId, int planId)
    {
        if (await HasActiveMembershipAsync(userId)) return ServiceResult.Failure("You already have an active membership.");
        var plan = await context.MembershipPlans.FirstOrDefaultAsync(p => p.Id == planId && !p.IsDeleted);
        if (plan == null) return ServiceResult.Failure("Membership plan was not found.");
        var start = DateTime.UtcNow;
        context.UserMemberships.Add(new UserMembership { UserId = userId, MembershipPlanId = plan.Id, StartDate = start, EndDate = start.AddDays(plan.DurationInDays), IsActive = true });
        await context.SaveChangesAsync(); return ServiceResult.Success("Membership activated successfully.");
    }
}
