using System.ComponentModel.DataAnnotations;
using static GymMembershipSystem.Data.Common.EntityValidationConstants.WorkoutClass;

namespace GymMembershipSystem.Data.Models;

public class WorkoutClass
{
    public int Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public int DurationMinutes { get; set; }

    public int Capacity { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;

    public int GymLocationId { get; set; }
    public GymLocation GymLocation { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
