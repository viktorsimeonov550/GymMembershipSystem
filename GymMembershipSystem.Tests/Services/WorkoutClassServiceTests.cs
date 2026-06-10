using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Implementations;
using GymMembershipSystem.Services.Models;
using GymMembershipSystem.Tests.Helpers;
using NUnit.Framework;

namespace GymMembershipSystem.Tests.Services;

[TestFixture]
public class WorkoutClassServiceTests
{
    [Test]
    public async Task AllAsyncReturnsOnlyNonDeletedWorkoutClasses()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var trainer = new Trainer { FullName = "Ivan Petrov", Specialization = "Fitness", Biography = "Fitness trainer biography.", ExperienceYears = 5, ImageUrl = "/trainer.jpg" };
        var location = new GymLocation { Name = "Gym Sofia", City = "Sofia", Address = "Center", ImageUrl = "/location.jpg" };
        context.Trainers.Add(trainer);
        context.GymLocations.Add(location);
        await context.SaveChangesAsync();
        context.WorkoutClasses.AddRange(
            new WorkoutClass { Title = "Morning Cardio", Description = "Cardio class description.", StartTime = DateTime.Now.AddDays(1), DurationMinutes = 45, Capacity = 20, ImageUrl = "/cardio.jpg", TrainerId = trainer.Id, GymLocationId = location.Id, IsDeleted = false },
            new WorkoutClass { Title = "Deleted Class", Description = "Deleted class description.", StartTime = DateTime.Now.AddDays(1), DurationMinutes = 45, Capacity = 20, ImageUrl = "/deleted.jpg", TrainerId = trainer.Id, GymLocationId = location.Id, IsDeleted = true });
        await context.SaveChangesAsync();

        var service = new WorkoutClassService(context);
        var result = (await service.AllAsync()).ToList();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Title, Is.EqualTo("Morning Cardio"));
    }

    [Test]
    public async Task DetailsAsyncReturnsWorkoutWithTrainerAndLocation()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var trainer = new Trainer { FullName = "Daniel Dimitrov", Specialization = "Boxing", Biography = "Boxing trainer biography.", ExperienceYears = 7, ImageUrl = "/trainer.jpg" };
        var location = new GymLocation { Name = "Gym Mladost", City = "Sofia", Address = "Mladost", ImageUrl = "/location.jpg" };
        context.Trainers.Add(trainer);
        context.GymLocations.Add(location);
        await context.SaveChangesAsync();
        var workout = new WorkoutClass { Title = "Boxing Basics", Description = "Boxing class description.", StartTime = DateTime.Now.AddDays(2), DurationMinutes = 60, Capacity = 12, ImageUrl = "/boxing.jpg", TrainerId = trainer.Id, GymLocationId = location.Id, IsDeleted = false };
        context.WorkoutClasses.Add(workout);
        await context.SaveChangesAsync();

        var service = new WorkoutClassService(context);
        var result = await service.DetailsAsync(workout.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Title, Is.EqualTo("Boxing Basics"));
        Assert.That(result.TrainerName, Is.EqualTo("Daniel Dimitrov"));
        Assert.That(result.LocationName, Is.EqualTo("Gym Mladost"));
    }

    [Test]
    public async Task DeleteAsyncSoftDeletesWorkoutClass()
    {
        await using var context = TestDbContextFactory.CreateContext();
        var trainer = new Trainer { FullName = "Elena Stoyanova", Specialization = "CrossFit", Biography = "CrossFit trainer biography.", ExperienceYears = 4, ImageUrl = "/trainer.jpg" };
        var location = new GymLocation { Name = "Gym Center", City = "Sofia", Address = "Center", ImageUrl = "/location.jpg" };
        context.Trainers.Add(trainer);
        context.GymLocations.Add(location);
        await context.SaveChangesAsync();
        var workout = new WorkoutClass { Title = "Crossfit Strength", Description = "Strength class description.", StartTime = DateTime.Now.AddDays(3), DurationMinutes = 50, Capacity = 16, ImageUrl = "/crossfit.jpg", TrainerId = trainer.Id, GymLocationId = location.Id, IsDeleted = false };
        context.WorkoutClasses.Add(workout);
        await context.SaveChangesAsync();

        var service = new WorkoutClassService(context);
        var deleted = await service.DeleteAsync(workout.Id);

        Assert.That(deleted, Is.True);
        Assert.That(workout.IsDeleted, Is.True);
    }
}
