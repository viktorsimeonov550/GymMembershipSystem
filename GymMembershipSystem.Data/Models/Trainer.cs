using System.ComponentModel.DataAnnotations;
using static GymMembershipSystem.Data.Common.EntityValidationConstants.Trainer;

namespace GymMembershipSystem.Data.Models;

public class Trainer
{
    public int Id { get; set; }

    [Required]
    [MaxLength(FullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [Required]
    [MaxLength(SpecializationMaxLength)]
    public string Specialization { get; set; } = null!;

    [Required]
    [MaxLength(BiographyMaxLength)]
    public string Biography { get; set; } = null!;

    public int ExperienceYears { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<WorkoutClass> WorkoutClasses { get; set; } = new List<WorkoutClass>();
}
