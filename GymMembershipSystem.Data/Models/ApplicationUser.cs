using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static GymMembershipSystem.Data.Common.EntityValidationConstants.ApplicationUser;

namespace GymMembershipSystem.Data.Models;

public class ApplicationUser : IdentityUser
{
    [MaxLength(FirstNameMaxLength)]
    public string? FirstName { get; set; }

    [MaxLength(LastNameMaxLength)]
    public string? LastName { get; set; }

    public int? Age { get; set; }

    public string? ProfileImageUrl { get; set; }

    public ICollection<UserMembership> Memberships { get; set; } = new List<UserMembership>();

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
