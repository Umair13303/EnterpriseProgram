using Microsoft.AspNetCore.Mvc;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.OSAUser.Controllers
{
    [Area(nameof(CommonRoute.RouteAreas.OSAUser))]
    public class OSUDashboardController : Controller
    {

        public IActionResult OSUDashboardDefault() => View();
    }
}
