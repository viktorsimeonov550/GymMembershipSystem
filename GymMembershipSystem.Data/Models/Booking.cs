namespace GymMembershipSystem.Data.Models;

public class Booking
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public int WorkoutClassId { get; set; }
    public WorkoutClass WorkoutClass { get; set; } = null!;
    public DateTime BookedOn { get; set; }
    public bool IsCancelled { get; set; }
}
