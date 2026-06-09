using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipSystem.Services.Implementations;

public class TrainerService : ITrainerService
{
    private readonly ApplicationDbContext context;
    public TrainerService(ApplicationDbContext context) => this.context = context;
    public async Task<IEnumerable<TrainerViewModel>> AllAsync() => await context.Trainers.Where(t => !t.IsDeleted).AsNoTracking().Select(t => new TrainerViewModel { Id = t.Id, FullName = t.FullName, Specialization = t.Specialization, Biography = t.Biography, ExperienceYears = t.ExperienceYears, ImageUrl = t.ImageUrl }).ToListAsync();
    public async Task<TrainerViewModel?> DetailsAsync(int id) => await context.Trainers.Where(t => t.Id == id && !t.IsDeleted).AsNoTracking().Select(t => new TrainerViewModel { Id = t.Id, FullName = t.FullName, Specialization = t.Specialization, Biography = t.Biography, ExperienceYears = t.ExperienceYears, ImageUrl = t.ImageUrl }).FirstOrDefaultAsync();
    public async Task<TrainerFormModel?> GetForEditAsync(int id) => await context.Trainers.Where(t => t.Id == id && !t.IsDeleted).AsNoTracking().Select(t => new TrainerFormModel { Id = t.Id, FullName = t.FullName, Specialization = t.Specialization, Biography = t.Biography, ExperienceYears = t.ExperienceYears, ImageUrl = t.ImageUrl }).FirstOrDefaultAsync();
    public async Task AddAsync(TrainerFormModel model) { context.Trainers.Add(new Trainer { FullName = model.FullName, Specialization = model.Specialization, Biography = model.Biography, ExperienceYears = model.ExperienceYears, ImageUrl = model.ImageUrl }); await context.SaveChangesAsync(); }
    public async Task<bool> EditAsync(TrainerFormModel model) { var e = await context.Trainers.FirstOrDefaultAsync(t => t.Id == model.Id && !t.IsDeleted); if (e == null) return false; e.FullName = model.FullName; e.Specialization = model.Specialization; e.Biography = model.Biography; e.ExperienceYears = model.ExperienceYears; e.ImageUrl = model.ImageUrl; await context.SaveChangesAsync(); return true; }
    public async Task<bool> DeleteAsync(int id) { var e = await context.Trainers.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted); if (e == null) return false; e.IsDeleted = true; await context.SaveChangesAsync(); return true; }
}
