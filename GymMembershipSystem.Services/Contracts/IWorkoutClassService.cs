using GymMembershipSystem.Services.Models;
namespace GymMembershipSystem.Services.Contracts;
public interface IWorkoutClassService
{
    Task<IEnumerable<WorkoutClassViewModel>> AllAsync(string? searchTerm = null);
    Task<WorkoutClassViewModel?> DetailsAsync(int id);
    Task<WorkoutClassFormModel?> GetForEditAsync(int id);
    Task AddAsync(WorkoutClassFormModel model);
    Task<bool> EditAsync(WorkoutClassFormModel model);
    Task<bool> DeleteAsync(int id);
}
