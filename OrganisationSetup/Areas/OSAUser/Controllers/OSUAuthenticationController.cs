using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.Common.HttpResponse;
using OrganisationSetup.Models.Common.ViewModel;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.Enums;

namespace OrganisationSetup.Areas.OSAUser.Controllers
{
    [Area(nameof(CommonRoute.RouteAreas.OSAUser))]
    public class OSUAuthenticationController : Controller
    {
        private IMSOrganisationSetupContext _iMSOSContext;
        private ISessionService _iSessionService;

        public OSUAuthenticationController(IMSOrganisationSetupContext iMSOSC, ISessionService iSessionService) 
        {
            _iMSOSContext = iMSOSC;
            _iSessionService = iSessionService;
        }
        #region PORTION CONTAIN CODE FOR : RENDERING VIEWS
        public IActionResult OSULogin() => View();
        public IActionResult OSULogout() => View(nameof(CommonRoute.RouteAction.OSULogin));
        #endregion


        #region PORTION CONTAIN CODE FOR : DATABASE OPERATION
        [HttpPost]
        public async Task<IActionResult> OSULoginValidate(OSUser postedData)
        {
            if (string.IsNullOrEmpty(postedData.UserName) || string.IsNullOrEmpty(postedData.Password))
            {
                ModelState.AddModelError(string.Empty, Models.Common.HttpResponse.HttpResponse.Message((int?)HttpResponseCode.NotFound));
                return View(nameof(OSULogin), postedData);
            }
            var user = await _iMSOSContext.OSUsers.FirstOrDefaultAsync(u => u.UserName == postedData.UserName && u.Password == postedData.Password);
            if (user != null)
            {
                var company = await _iMSOSContext.OSCompanies.FirstOrDefaultAsync(c => c.Id == user.CompanyId);
                var branch = await _iMSOSContext.OSBranches.FirstOrDefaultAsync(b => b.Id == user.BranchId);
                #region IN CASE: USER LOGIN SUCCESS -- GET RIGHTS
                if (user != null)
                {
                    var rightList = await _iMSOSContext.Rights.AsNoTracking().ToListAsync();
                    var userRightList = rightList.Where(r => !string.IsNullOrEmpty(r.RoleIds) && r.RoleIds.Split(',').Contains(user.RoleId.ToString()))
                        .GroupBy(r => r.Menu)
                        .Select(menuGroup => new VMMenu
                        {
                            Menu = menuGroup.Key,
                            SubMenu = menuGroup.GroupBy(r => r.SubMenu).Select(subMenuGroup => new VMSubMenu
                            {
                                SubMenu = subMenuGroup.Key,
                                Rights = subMenuGroup.Select(r => new VMRight
                                {
                                    DisplayName = r.DisplayName,
                                    Url = r.FormName
                                }).ToList()
                            }).ToList()
                        }).ToList();

                    _iSessionService.SetSessionInformation(
                                user.Id,
                                user.UserName ?? "User",
                                company?.CompanyName ?? "Inventory Cloth",
                                company?.Logo ?? "/images/default-logo.png",
                                userRightList
                            );

                    return RedirectToAction(nameof(CommonRoute.RouteAction.OSUDashboardDefault), nameof(CommonRoute.RouteController.OSUDashboard), new { area = nameof(CommonRoute.RouteAreas.OSAUser) });
                }
                #endregion
            }
            ModelState.AddModelError(string.Empty, Models.Common.HttpResponse.HttpResponse.Message((int?)HttpResponseCode.BadRequest));
            return View(nameof(OSULogin), postedData);
        }
        #endregion
    }
}
