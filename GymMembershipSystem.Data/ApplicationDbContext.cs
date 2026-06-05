using GymMembershipSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GymMembershipSystem.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<MembershipPlan> MembershipPlans { get; set; } = null!;
    public DbSet<UserMembership> UserMemberships { get; set; } = null!;
    public DbSet<Trainer> Trainers { get; set; } = null!;
    public DbSet<WorkoutClass> WorkoutClasses { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<GymLocation> GymLocations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<MembershipPlan>().Property(p => p.Price).HasPrecision(18, 2);

        builder.Entity<Booking>()
            .HasIndex(b => new { b.UserId, b.WorkoutClassId })
            .IsUnique(false);

        builder.Entity<MembershipPlan>().HasData(
            new MembershipPlan { Id = 1, Name = "Basic", Description = "Unlimited gym access, lockers, showers and Wi-Fi for members who want simple training access.", Price = 39.99m, DurationInDays = 30, ImageUrl = "/images/basic.jpg", IsDeleted = false },
            new MembershipPlan { Id = 2, Name = "Standard", Description = "Gym access plus group activities, boxing area, crossfit zone, lockers, showers and Wi-Fi.", Price = 59.99m, DurationInDays = 30, ImageUrl = "/images/standard.jpg", IsDeleted = false },
            new MembershipPlan { Id = 3, Name = "Premium", Description = "Full access with group classes, MMA cage, sauna, protein bar benefits, trainer discount and selected free parking.", Price = 89.99m, DurationInDays = 30, ImageUrl = "/images/premium.jpg", IsDeleted = false }
        );

        builder.Entity<Trainer>().HasData(
            new Trainer { Id = 1, FullName = "Ivan Petrov", Specialization = "Fitness", Biography = "Certified trainer focused on strength and conditioning.", ExperienceYears = 6, ImageUrl = "/images/trainer1.jpg", IsDeleted = false },
            new Trainer { Id = 2, FullName = "Maria Georgieva", Specialization = "Yoga", Biography = "Yoga instructor focused on mobility, balance and recovery.", ExperienceYears = 5, ImageUrl = "/images/trainer2.jpg", IsDeleted = false },
            new Trainer { Id = 3, FullName = "Daniel Dimitrov", Specialization = "Boxing", Biography = "Boxing coach with beginner and advanced class experience.", ExperienceYears = 7, ImageUrl = "/images/trainer3.jpg", IsDeleted = false },
            new Trainer { Id = 4, FullName = "Elena Stoyanova", Specialization = "CrossFit", Biography = "Functional training coach focused on conditioning, mobility and explosive strength.", ExperienceYears = 4, ImageUrl = "/images/trainer4.jpg", IsDeleted = false }
        );

        builder.Entity<GymLocation>().HasData(
            new GymLocation { Id = 1, Name = "Gym Sofia Center", City = "Sofia", Address = "Vitosha Blvd 10", ImageUrl = "/images/location1.jpg", IsDeleted = false },
            new GymLocation { Id = 2, Name = "Gym Mladost", City = "Sofia", Address = "Mladost 1A", ImageUrl = "/images/location2.jpg", IsDeleted = false },
            new GymLocation { Id = 3, Name = "Gym Studentski Grad", City = "Sofia", Address = "Studentski Grad 22", ImageUrl = "/images/location3.jpg", IsDeleted = false }
        );

        builder.Entity<WorkoutClass>().HasData(
            new WorkoutClass { Id = 1, Title = "Morning Cardio", Description = "High-energy cardio workout for all levels.", StartTime = new DateTime(2026, 7, 1, 8, 0, 0), DurationMinutes = 45, Capacity = 20, ImageUrl = "/images/cardio.jpg", TrainerId = 1, GymLocationId = 1, IsDeleted = false },
            new WorkoutClass { Id = 2, Title = "Yoga Flow", Description = "Mobility and flexibility class with controlled breathing.", StartTime = new DateTime(2026, 7, 2, 18, 0, 0), DurationMinutes = 60, Capacity = 15, ImageUrl = "/images/yoga.jpg", TrainerId = 2, GymLocationId = 2, IsDeleted = false },
            new WorkoutClass { Id = 3, Title = "Boxing Basics", Description = "Beginner-friendly boxing class with technique drills.", StartTime = new DateTime(2026, 7, 3, 19, 0, 0), DurationMinutes = 60, Capacity = 12, ImageUrl = "/images/boxing.jpg", TrainerId = 3, GymLocationId = 1, IsDeleted = false },
            new WorkoutClass { Id = 4, Title = "MMA Cage Conditioning", Description = "Combat conditioning class using the MMA cage, bodyweight drills and explosive rounds.", StartTime = new DateTime(2026, 7, 4, 18, 30, 0), DurationMinutes = 60, Capacity = 10, ImageUrl = "/images/mma.jpg", TrainerId = 3, GymLocationId = 3, IsDeleted = false },
            new WorkoutClass { Id = 5, Title = "Crossfit Strength", Description = "Functional strength session in the crossfit area with circuits and free weights.", StartTime = new DateTime(2026, 7, 5, 17, 30, 0), DurationMinutes = 50, Capacity = 16, ImageUrl = "/images/crossfit.jpg", TrainerId = 4, GymLocationId = 2, IsDeleted = false }
        );
    }
}