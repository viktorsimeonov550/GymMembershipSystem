using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace GymMembershipSystem.Web.Controllers;
[Authorize]
public class BookingsController : Controller
{
    private readonly IBookingService service;
    public BookingsController(IBookingService service) => this.service = service;
    public async Task<IActionResult> Mine() => View(await service.MineAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(int id) { var result = await service.BookAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!, id); TempData[result.Succeeded ? "SuccessMessage" : "ErrorMessage"] = result.Message; return RedirectToAction("All", "WorkoutClasses"); }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id) { var result = await service.CancelAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!, id); TempData[result.Succeeded ? "SuccessMessage" : "ErrorMessage"] = result.Message; return RedirectToAction(nameof(Mine)); }
}
