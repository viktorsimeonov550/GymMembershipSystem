using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Common;
using GymMembershipSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipSystem.Web.Infrastructure;

public static class ApplicationSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Makes the project run immediately with LocalDB.
        // For final defense, you can still add migrations and use Update-Database.
        // Transient connection failures (e.g. a cold LocalDB start) are retried
        // automatically by the EnableRetryOnFailure execution strategy configured
        // on the DbContext in Program.cs.
        await context.Database.EnsureCreatedAsync();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        if (!await roleManager.RoleExistsAsync(ApplicationConstants.AdministratorRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(ApplicationConstants.AdministratorRoleName));
        }

        var admin = await userManager.FindByEmailAsync(ApplicationConstants.AdminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = ApplicationConstants.AdminEmail,
                Email = ApplicationConstants.AdminEmail,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, ApplicationConstants.AdminPassword);
        }

        if (!await userManager.IsInRoleAsync(admin, ApplicationConstants.AdministratorRoleName))
        {
            await userManager.AddToRoleAsync(admin, ApplicationConstants.AdministratorRoleName);
        }
    }
}
