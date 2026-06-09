using GymMembershipSystem.Data;
using GymMembershipSystem.Data.Models;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Implementations;
using GymMembershipSystem.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Error/Unauthorized401";
});


builder.Services.AddScoped<IMembershipPlanService, MembershipPlanService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IWorkoutClassService, WorkoutClassService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUserMembershipService, UserMembershipService>();
builder.Services.AddScoped<IGymLocationService, GymLocationService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await ApplicationSeeder.SeedAsync(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error/InternalServerError500");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/Handle", "?code={0}");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
