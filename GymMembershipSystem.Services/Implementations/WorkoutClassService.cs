using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.EntityFrameworkCore;
namespace GymMembershipSystem.Services.Implementations;
public class WorkoutClassService : IWorkoutClassService
{
    private readonly ApplicationDbContext context;
    public WorkoutClassService(ApplicationDbContext context) => this.context = context;
    public async Task<IEnumerable<WorkoutClassViewModel>> AllAsync(string? searchTerm = null)
    {
        var query = context.WorkoutClasses.Where(w => !w.IsDeleted);
        if (!string.IsNullOrWhiteSpace(searchTerm)) query = query.Where(w => w.Title.Contains(searchTerm) || w.Description.Contains(searchTerm));
        return await query.AsNoTracking().Select(w => new WorkoutClassViewModel { Id = w.Id, Title = w.Title, Description = w.Description, StartTime = w.StartTime, DurationMinutes = w.DurationMinutes, Capacity = w.Capacity, BookedPlaces = w.Bookings.Count(b => !b.IsCancelled), TrainerName = w.Trainer.FullName, LocationName = w.GymLocation.Name, ImageUrl = w.ImageUrl }).ToListAsync();
    }
    public async Task<WorkoutClassViewModel?> DetailsAsync(int id) => await context.WorkoutClasses.Where(w => w.Id == id && !w.IsDeleted).AsNoTracking().Select(w => new WorkoutClassViewModel { Id = w.Id, Title = w.Title, Description = w.Description, StartTime = w.StartTime, DurationMinutes = w.DurationMinutes, Capacity = w.Capacity, BookedPlaces = w.Bookings.Count(b => !b.IsCancelled), TrainerName = w.Trainer.FullName, LocationName = w.GymLocation.Name, ImageUrl = w.ImageUrl }).FirstOrDefaultAsync();
    public async Task<WorkoutClassFormModel?> GetForEditAsync(int id) => await context.WorkoutClasses.Where(w => w.Id == id && !w.IsDeleted).AsNoTracking().Select(w => new WorkoutClassFormModel { Id = w.Id, Title = w.Title, Description = w.Description, StartTime = w.StartTime, DurationMinutes = w.DurationMinutes, Capacity = w.Capacity, ImageUrl = w.ImageUrl, TrainerId = w.TrainerId, GymLocationId = w.GymLocationId }).FirstOrDefaultAsync();
    public async Task AddAsync(WorkoutClassFormModel model) { context.WorkoutClasses.Add(new WorkoutClass { Title = model.Title, Description = model.Description, StartTime = model.StartTime, DurationMinutes = model.DurationMinutes, Capacity = model.Capacity, ImageUrl = model.ImageUrl, TrainerId = model.TrainerId, GymLocationId = model.GymLocationId }); await context.SaveChangesAsync(); }
    public async Task<bool> EditAsync(WorkoutClassFormModel model) { var e = await context.WorkoutClasses.FirstOrDefaultAsync(w => w.Id == model.Id && !w.IsDeleted); if (e == null) return false; e.Title = model.Title; e.Description = model.Description; e.StartTime = model.StartTime; e.DurationMinutes = model.DurationMinutes; e.Capacity = model.Capacity; e.ImageUrl = model.ImageUrl; e.TrainerId = model.TrainerId; e.GymLocationId = model.GymLocationId; await context.SaveChangesAsync(); return true; }
    public async Task<bool> DeleteAsync(int id) { var e = await context.WorkoutClasses.FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted); if (e == null) return false; e.IsDeleted = true; await context.SaveChangesAsync(); return true; }
}
