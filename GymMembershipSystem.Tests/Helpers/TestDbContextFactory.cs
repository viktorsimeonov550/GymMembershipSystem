using GymMembershipSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipSystem.Tests.Helpers;

public static class TestDbContextFactory
{
    public static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
