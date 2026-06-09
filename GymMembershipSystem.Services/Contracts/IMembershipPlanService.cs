using GymMembershipSystem.Services.Models;
namespace GymMembershipSystem.Services.Contracts;
public interface IMembershipPlanService
{
    Task<IEnumerable<MembershipPlanViewModel>> AllAsync();
    Task<MembershipPlanViewModel?> DetailsAsync(int id);
    Task<MembershipPlanFormModel?> GetForEditAsync(int id);
    Task AddAsync(MembershipPlanFormModel model);
    Task<bool> EditAsync(MembershipPlanFormModel model);
    Task<bool> DeleteAsync(int id);
}
