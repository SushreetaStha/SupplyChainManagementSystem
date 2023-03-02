using Microsoft.AspNetCore.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
