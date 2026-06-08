using GymMembershipSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace GymMembershipSystem.Web.Controllers;
public class TrainersController : Controller
{
    private readonly ITrainerService service;
    public TrainersController(ITrainerService service) => this.service = service;
    public async Task<IActionResult> All() => View(await service.AllAsync());
    public async Task<IActionResult> Details(int id) { var model = await service.DetailsAsync(id); return model == null ? NotFound() : View(model); }
}
