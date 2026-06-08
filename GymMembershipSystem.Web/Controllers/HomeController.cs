using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly IStatisticsService statisticsService;
    public HomeController(IStatisticsService statisticsService) => this.statisticsService = statisticsService;
    public async Task<IActionResult> Index() => View(await statisticsService.GetDashboardStatisticsAsync());
    public IActionResult About() => View();
}
