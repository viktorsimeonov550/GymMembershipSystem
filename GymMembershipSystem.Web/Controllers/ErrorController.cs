using Microsoft.AspNetCore.Mvc;

namespace GymMembershipSystem.Web.Controllers;

public class ErrorController : Controller
{
    [Route("Error/Handle")]
    public IActionResult Handle(int code)
    {
        return code switch
        {
            400 => View("BadRequest400"),
            401 => View("Unauthorized401"),
            404 => View("NotFound404"),
            _ => View("InternalServerError500")
        };
    }

    [Route("Error/InternalServerError500")]
    public IActionResult InternalServerError500() => View();
}
