using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShopFlower.Controllers
{
    [Authorize]
    public class MyAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

            return Ok(new
            {
                UserId = userId,
                UserEmail = userEmail
            });
        }
    }
}
