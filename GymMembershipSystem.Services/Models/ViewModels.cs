using System.ComponentModel.DataAnnotations;

namespace GymMembershipSystem.Services.Models;

public class MembershipPlanViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public string ImageUrl { get; set; } = null!;
}

public class MembershipPlanFormModel
{
    public int Id { get; set; }
    [Required, StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;
    [Required, StringLength(500, MinimumLength = 10)]
    public string Description { get; set; } = null!;
    [Range(0.01, 1000)]
    public decimal Price { get; set; }
    [Range(1, 365)]
    public int DurationInDays { get; set; } = 30;
    [Required]
    public string ImageUrl { get; set; } = null!;
}

public class TrainerViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public int ExperienceYears { get; set; }
    public string ImageUrl { get; set; } = null!;
}

public class TrainerFormModel
{
    public int Id { get; set; }
    [Required, StringLength(80, MinimumLength = 5)]
    public string FullName { get; set; } = null!;
    [Required, StringLength(50, MinimumLength = 3)]
    public string Specialization { get; set; } = null!;
    [Required, StringLength(1000, MinimumLength = 20)]
    public string Biography { get; set; } = null!;
    [Range(0, 60)]
    public int ExperienceYears { get; set; }
    [Required]
    public string ImageUrl { get; set; } = null!;
}

public class GymLocationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
}

public class GymLocationFormModel
{
    public int Id { get; set; }
    [Required, StringLength(80, MinimumLength = 3)]
    public string Name { get; set; } = null!;
    [Required, StringLength(60, MinimumLength = 2)]
    public string City { get; set; } = null!;
    [Required, StringLength(150, MinimumLength = 5)]
    public string Address { get; set; } = null!;
    [Required]
    public string ImageUrl { get; set; } = null!;
}

public class WorkoutClassViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public int Capacity { get; set; }
    public int BookedPlaces { get; set; }
    public string TrainerName { get; set; } = null!;
    public string LocationName { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}

public class WorkoutClassFormModel
{
    public int Id { get; set; }
    [Required, StringLength(80, MinimumLength = 3)]
    public string Title { get; set; } = null!;
    [Required, StringLength(500, MinimumLength = 10)]
    public string Description { get; set; } = null!;
    public DateTime StartTime { get; set; } = DateTime.Now.AddDays(1);
    [Range(15, 180)]
    public int DurationMinutes { get; set; } = 60;
    [Range(1, 100)]
    public int Capacity { get; set; } = 20;
    [Required]
    public string ImageUrl { get; set; } = null!;
    [Range(1, int.MaxValue)]
    public int TrainerId { get; set; }
    [Range(1, int.MaxValue)]
    public int GymLocationId { get; set; }
}

public class BookingViewModel
{
    public int Id { get; set; }
    public string WorkoutTitle { get; set; } = null!;
    public string TrainerName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public bool IsCancelled { get; set; }
}

public class MyMembershipViewModel
{
    public string PlanName { get; set; } = "No active membership";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}

public class DashboardStatisticsViewModel
{
    public int TotalPlans { get; set; }
    public int TotalTrainers { get; set; }
    public int TotalWorkoutClasses { get; set; }
    public int TotalBookings { get; set; }
    public int ActiveMemberships { get; set; }
}
