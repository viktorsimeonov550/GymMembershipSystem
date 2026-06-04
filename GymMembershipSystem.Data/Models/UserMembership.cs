namespace GymMembershipSystem.Data.Models;

public class UserMembership
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public int MembershipPlanId { get; set; }
    public MembershipPlan MembershipPlan { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}
