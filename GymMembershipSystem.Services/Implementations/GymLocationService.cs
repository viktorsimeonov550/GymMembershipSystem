using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.EntityFrameworkCore;
namespace GymMembershipSystem.Services.Implementations;
public class GymLocationService : IGymLocationService
{
    private readonly ApplicationDbContext context;
    public GymLocationService(ApplicationDbContext context) => this.context = context;
    public async Task<IEnumerable<GymLocationViewModel>> AllAsync() => await context.GymLocations.Where(l => !l.IsDeleted).AsNoTracking().Select(l => new GymLocationViewModel { Id = l.Id, Name = l.Name, City = l.City, Address = l.Address, ImageUrl = l.ImageUrl }).ToListAsync();
    public async Task<GymLocationFormModel?> GetForEditAsync(int id) => await context.GymLocations.Where(l => l.Id == id && !l.IsDeleted).AsNoTracking().Select(l => new GymLocationFormModel { Id = l.Id, Name = l.Name, City = l.City, Address = l.Address, ImageUrl = l.ImageUrl }).FirstOrDefaultAsync();
    public async Task AddAsync(GymLocationFormModel model) { context.GymLocations.Add(new GymLocation { Name = model.Name, City = model.City, Address = model.Address, ImageUrl = model.ImageUrl }); await context.SaveChangesAsync(); }
    public async Task<bool> EditAsync(GymLocationFormModel model) { var e = await context.GymLocations.FirstOrDefaultAsync(l => l.Id == model.Id && !l.IsDeleted); if (e == null) return false; e.Name = model.Name; e.City = model.City; e.Address = model.Address; e.ImageUrl = model.ImageUrl; await context.SaveChangesAsync(); return true; }
    public async Task<bool> DeleteAsync(int id) { var e = await context.GymLocations.FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted); if (e == null) return false; e.IsDeleted = true; await context.SaveChangesAsync(); return true; }
}
