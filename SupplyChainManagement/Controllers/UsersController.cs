using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Models;
using SupplyChainManagement.Services;
using SupplyChainManagement.ViewModels.Users;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(AuthenticateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var res = await _userService.Authenticate(model);

            if (res == null)
            {
                ModelState.AddModelError("Password", "Username or Password incorrect");
                return View("SignIn");
            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("token", res.Token, options);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return RedirectToAction("SignIn");
        }
    }
}
