using GymMembershipSystem.Data.Common;
using GymMembershipSystem.Services.Contracts;
using GymMembershipSystem.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipSystem.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = ApplicationConstants.AdministratorRoleName)]
public class MembershipPlansController : Controller
{
    private readonly IMembershipPlanService service;
    public MembershipPlansController(IMembershipPlanService service) => this.service = service;
    public async Task<IActionResult> All() => View(await service.AllAsync());
    public IActionResult Add() => View(new MembershipPlanFormModel());
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(MembershipPlanFormModel model) { if (!ModelState.IsValid) return View(model); await service.AddAsync(model); TempData["SuccessMessage"] = "Record added successfully."; return RedirectToAction(nameof(All)); }
    public async Task<IActionResult> Edit(int id) { var model = await service.GetForEditAsync(id); return model == null ? NotFound() : View(model); }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MembershipPlanFormModel model) { if (!ModelState.IsValid) return View(model); var ok = await service.EditAsync(model); if (!ok) return NotFound(); TempData["SuccessMessage"] = "Record edited successfully."; return RedirectToAction(nameof(All)); }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id) { var ok = await service.DeleteAsync(id); if (!ok) return NotFound(); TempData["SuccessMessage"] = "Record deleted successfully."; return RedirectToAction(nameof(All)); }
}
