using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace GymMembershipSystem.Web.Controllers.Api;
[ApiController]
[Route("api/statistics")]
public class StatisticsApiController : ControllerBase
{
    private readonly IStatisticsService service;
    public StatisticsApiController(IStatisticsService service) => this.service = service;
    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard() => Ok(await service.GetDashboardStatisticsAsync());
}
