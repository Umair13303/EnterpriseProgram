using Microsoft.AspNetCore.Mvc;

namespace IMCClothHouse.Areas.CHAInventory.Controllers
{
    [Area("CHAInventory")] // 1. Always define the Area explicitly
    public class CHIProductCategoryController : Controller
    {
        public IActionResult CreateUpdate_ProductCategory()
        {
            return View();
        }
    }
}
