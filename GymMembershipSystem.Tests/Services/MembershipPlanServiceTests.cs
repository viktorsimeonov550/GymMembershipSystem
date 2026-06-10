using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Implementations;
using GymMembershipSystem.Services.Models;
using GymMembershipSystem.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GymMembershipSystem.Tests.Services;

[TestFixture]
public class MembershipPlanServiceTests
{
    [Test]
    public async Task AllAsyncReturnsOnlyNonDeletedPlans()
    {
        await using var context = TestDbContextFactory.CreateContext();
        context.MembershipPlans.AddRange(
            new MembershipPlan { Name = "Basic", Description = "Basic membership plan", Price = 30, DurationInDays = 30, ImageUrl = "/basic.jpg", IsDeleted = false },
            new MembershipPlan { Name = "Deleted", Description = "Deleted membership plan", Price = 40, DurationInDays = 30, ImageUrl = "/deleted.jpg", IsDeleted = true });
        await context.SaveChangesAsync();

        var service = new MembershipPlanService(context);
        var result = (await service.AllAsync()).ToList();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Basic"));
    }

    [Test]
    public async Task DetailsAsyncReturnsCorrectPlanById()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var plan = new MembershipPlan { Name = "Premium", Description = "Premium membership plan", Price = 90, DurationInDays = 30, ImageUrl = "/premium.jpg", IsDeleted = false };
        context.MembershipPlans.Add(plan);
        await context.SaveChangesAsync();

        var service = new MembershipPlanService(context);
        var result = await service.DetailsAsync(plan.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Premium"));
    }

    [Test]
    public async Task AddAsyncCreatesNewMembershipPlan()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var service = new MembershipPlanService(context);

        await service.AddAsync(new MembershipPlanFormModel
        {
            Name = "Student",
            Description = "Student membership plan",
            Price = 25,
            DurationInDays = 30,
            ImageUrl = "/student.jpg"
        });

        Assert.That(await context.MembershipPlans.CountAsync(), Is.EqualTo(1));
        Assert.That(await context.MembershipPlans.AnyAsync(p => p.Name == "Student"), Is.True);
    }

    [Test]
    public async Task EditAsyncUpdatesExistingPlan()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var plan = new MembershipPlan { Name = "Old", Description = "Old membership plan", Price = 10, DurationInDays = 30, ImageUrl = "/old.jpg", IsDeleted = false };
        context.MembershipPlans.Add(plan);
        await context.SaveChangesAsync();

        var service = new MembershipPlanService(context);
        var edited = await service.EditAsync(new MembershipPlanFormModel
        {
            Id = plan.Id,
            Name = "Updated",
            Description = "Updated membership plan",
            Price = 55,
            DurationInDays = 60,
            ImageUrl = "/updated.jpg"
        });

        Assert.That(edited, Is.True);
        Assert.That(plan.Name, Is.EqualTo("Updated"));
        Assert.That(plan.Price, Is.EqualTo(55));
    }

    [Test]
    public async Task DeleteAsyncSoftDeletesPlan()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var plan = new MembershipPlan { Name = "Basic", Description = "Basic membership plan", Price = 30, DurationInDays = 30, ImageUrl = "/basic.jpg", IsDeleted = false };
        context.MembershipPlans.Add(plan);
        await context.SaveChangesAsync();

        var service = new MembershipPlanService(context);
        var deleted = await service.DeleteAsync(plan.Id);

        Assert.That(deleted, Is.True);
        Assert.That(plan.IsDeleted, Is.True);
    }
}
