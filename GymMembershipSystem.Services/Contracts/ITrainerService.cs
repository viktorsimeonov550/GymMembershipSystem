using GymMembershipSystem.Services.Models;
namespace GymMembershipSystem.Services.Contracts;
public interface ITrainerService
{
    Task<IEnumerable<TrainerViewModel>> AllAsync();
    Task<TrainerViewModel?> DetailsAsync(int id);
    Task<TrainerFormModel?> GetForEditAsync(int id);
    Task AddAsync(TrainerFormModel model);
    Task<bool> EditAsync(TrainerFormModel model);
    Task<bool> DeleteAsync(int id);
}
