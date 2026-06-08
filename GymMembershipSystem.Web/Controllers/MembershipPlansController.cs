using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymMembershipSystem.Web.Controllers;

public class MembershipPlansController : Controller
{
    private readonly IMembershipPlanService planService;
    private readonly IUserMembershipService membershipService;
    public MembershipPlansController(IMembershipPlanService planService, IUserMembershipService membershipService) { this.planService = planService; this.membershipService = membershipService; }
    public async Task<IActionResult> All() => View(await planService.AllAsync());
    public async Task<IActionResult> Details(int id) { var model = await planService.DetailsAsync(id); return model == null ? NotFound() : View(model); }
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subscribe(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await membershipService.SubscribeAsync(userId, id);
        TempData[result.Succeeded ? "SuccessMessage" : "ErrorMessage"] = result.Message;
        return RedirectToAction(nameof(All));
    }
}
