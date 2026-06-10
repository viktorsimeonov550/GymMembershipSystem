using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly IStatisticsService statisticsService;
    private readonly IGymLocationService gymLocationService;

    public HomeController(IStatisticsService statisticsService, IGymLocationService gymLocationService)
    {
        this.statisticsService = statisticsService;
        this.gymLocationService = gymLocationService;
    }

    public async Task<IActionResult> Index() => View(await statisticsService.GetDashboardStatisticsAsync());
    public async Task<IActionResult> About() => View(await gymLocationService.AllAsync());
}
