using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace GymMembershipSystem.Web.Controllers;
[Authorize]
public class MyMembershipController : Controller
{
    private readonly IUserMembershipService service;
    public MyMembershipController(IUserMembershipService service) => this.service = service;
    public async Task<IActionResult> Index() => View(await service.MineAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
}
