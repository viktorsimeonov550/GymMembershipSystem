using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Implementations;
using GymMembershipSystem.Services.Models;
using GymMembershipSystem.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GymMembershipSystem.Tests.Services;

[TestFixture]
public class TrainerServiceTests
{
    [Test]
    public async Task AllAsyncReturnsOnlyNonDeletedTrainers()
    {
        await using var context = TestDbContextFactory.CreateContext();
        context.Trainers.AddRange(
            new Trainer { FullName = "Ivan Petrov", Specialization = "Fitness", Biography = "Certified trainer with experience.", ExperienceYears = 5, ImageUrl = "/ivan.jpg", IsDeleted = false },
            new Trainer { FullName = "Deleted Trainer", Specialization = "Yoga", Biography = "Deleted trainer biography.", ExperienceYears = 3, ImageUrl = "/deleted.jpg", IsDeleted = true });
        await context.SaveChangesAsync();

        var service = new TrainerService(context);
        var result = (await service.AllAsync()).ToList();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].FullName, Is.EqualTo("Ivan Petrov"));
    }

    [Test]
    public async Task AddAsyncCreatesTrainer()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var service = new TrainerService(context);

        await service.AddAsync(new TrainerFormModel
        {
            FullName = "Maria Georgieva",
            Specialization = "Yoga",
            Biography = "Yoga instructor focused on mobility and recovery.",
            ExperienceYears = 4,
            ImageUrl = "/maria.jpg"
        });

        Assert.That(await context.Trainers.CountAsync(), Is.EqualTo(1));
        Assert.That(await context.Trainers.AnyAsync(t => t.FullName == "Maria Georgieva"), Is.True);
    }

    [Test]
    public async Task DeleteAsyncSoftDeletesTrainer()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var trainer = new Trainer { FullName = "Daniel Dimitrov", Specialization = "Boxing", Biography = "Boxing trainer with class experience.", ExperienceYears = 7, ImageUrl = "/daniel.jpg", IsDeleted = false };
        context.Trainers.Add(trainer);
        await context.SaveChangesAsync();

        var service = new TrainerService(context);
        var deleted = await service.DeleteAsync(trainer.Id);

        Assert.That(deleted, Is.True);
        Assert.That(trainer.IsDeleted, Is.True);
    }
}
