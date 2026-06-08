using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace GymMembershipSystem.Web.Controllers;
public class WorkoutClassesController : Controller
{
    private readonly IWorkoutClassService service;
    public WorkoutClassesController(IWorkoutClassService service) => this.service = service;
    public async Task<IActionResult> All(string? searchTerm) { ViewBag.SearchTerm = searchTerm; return View(await service.AllAsync(searchTerm)); }
    public async Task<IActionResult> Details(int id) { var model = await service.DetailsAsync(id); return model == null ? NotFound() : View(model); }
}
