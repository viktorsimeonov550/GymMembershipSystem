using System.ComponentModel.DataAnnotations;
using static GymMembershipSystem.Data.Common.EntityValidationConstants.MembershipPlan;

namespace GymMembershipSystem.Data.Models;

public class MembershipPlan
{
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int DurationInDays { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
