namespace GymMembershipSystem.Data.Common;

public static class EntityValidationConstants
{
    public static class ApplicationUser
    {
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 50;

        public const int LastNameMinLength = 2;
        public const int LastNameMaxLength = 50;
    }

    public static class MembershipPlan
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 50;
        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 500;
    }

    public static class Trainer
    {
        public const int FullNameMinLength = 5;
        public const int FullNameMaxLength = 80;
        public const int SpecializationMinLength = 3;
        public const int SpecializationMaxLength = 50;
        public const int BiographyMinLength = 20;
        public const int BiographyMaxLength = 1000;
    }

    public static class WorkoutClass
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 80;
        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 500;
    }

    public static class GymLocation
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 80;
        public const int CityMinLength = 2;
        public const int CityMaxLength = 60;
        public const int AddressMinLength = 5;
        public const int AddressMaxLength = 150;
    }
}
