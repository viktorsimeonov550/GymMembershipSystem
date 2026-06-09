using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipSystem.Services.Implementations;

public class MembershipPlanService : IMembershipPlanService
{
    private readonly ApplicationDbContext context;
    public MembershipPlanService(ApplicationDbContext context) => this.context = context;

    public async Task<IEnumerable<MembershipPlanViewModel>> AllAsync()
        => await context.MembershipPlans.Where(p => !p.IsDeleted).AsNoTracking()
            .Select(p => new MembershipPlanViewModel { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, DurationInDays = p.DurationInDays, ImageUrl = p.ImageUrl })
            .ToListAsync();

    public async Task<MembershipPlanViewModel?> DetailsAsync(int id)
        => await context.MembershipPlans.Where(p => p.Id == id && !p.IsDeleted).AsNoTracking()
            .Select(p => new MembershipPlanViewModel { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, DurationInDays = p.DurationInDays, ImageUrl = p.ImageUrl })
            .FirstOrDefaultAsync();

    public async Task<MembershipPlanFormModel?> GetForEditAsync(int id)
        => await context.MembershipPlans.Where(p => p.Id == id && !p.IsDeleted).AsNoTracking()
            .Select(p => new MembershipPlanFormModel { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, DurationInDays = p.DurationInDays, ImageUrl = p.ImageUrl })
            .FirstOrDefaultAsync();

    public async Task AddAsync(MembershipPlanFormModel model)
    {
        context.MembershipPlans.Add(new MembershipPlan { Name = model.Name, Description = model.Description, Price = model.Price, DurationInDays = model.DurationInDays, ImageUrl = model.ImageUrl });
        await context.SaveChangesAsync();
    }

    public async Task<bool> EditAsync(MembershipPlanFormModel model)
    {
        var entity = await context.MembershipPlans.FirstOrDefaultAsync(p => p.Id == model.Id && !p.IsDeleted);
        if (entity == null) return false;
        entity.Name = model.Name; entity.Description = model.Description; entity.Price = model.Price; entity.DurationInDays = model.DurationInDays; entity.ImageUrl = model.ImageUrl;
        await context.SaveChangesAsync(); return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.MembershipPlans.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        if (entity == null) return false;
        entity.IsDeleted = true; await context.SaveChangesAsync(); return true;
    }
}
