using GymMembershipSystem.Services.Models;
namespace GymMembershipSystem.Services.Contracts;
public interface IGymLocationService
{
    Task<IEnumerable<GymLocationViewModel>> AllAsync();
    Task<GymLocationFormModel?> GetForEditAsync(int id);
    Task AddAsync(GymLocationFormModel model);
    Task<bool> EditAsync(GymLocationFormModel model);
    Task<bool> DeleteAsync(int id);
}
