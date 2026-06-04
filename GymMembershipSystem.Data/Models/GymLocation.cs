using System.ComponentModel.DataAnnotations;
using static GymMembershipSystem.Data.Common.EntityValidationConstants.GymLocation;

namespace GymMembershipSystem.Data.Models;

public class GymLocation
{
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(CityMaxLength)]
    public string City { get; set; } = null!;

    [Required]
    [MaxLength(AddressMaxLength)]
    public string Address { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<WorkoutClass> WorkoutClasses { get; set; } = new List<WorkoutClass>();
}
