using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Common;
using GymMembershipSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipSystem.Web.Infrastructure;

public static class ApplicationSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Makes the project run immediately with LocalDB.
        // For final defense, you can still add migrations and use Update-Database.
        // LocalDB can be slow to start on a cold launch, so the first connection
        // sometimes fails with "server was not found". Retry a few times instead
        // of letting that transient error crash startup.
        const int maxAttempts = 5;
        for (int attempt = 1; ; attempt++)
        {
            try
            {
                await context.Database.EnsureCreatedAsync();
                break;
            }
            catch (SqlException) when (attempt < maxAttempts)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

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
