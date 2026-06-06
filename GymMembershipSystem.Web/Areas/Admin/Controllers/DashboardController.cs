using GymMembershipSystem.Data.Common;
using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GymMembershipSystem.Web.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = ApplicationConstants.AdministratorRoleName)]
public class DashboardController : Controller
{
    private readonly IStatisticsService service;
    public DashboardController(IStatisticsService service) => this.service = service;
    public async Task<IActionResult> Index() => View(await service.GetDashboardStatisticsAsync());
}
