using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4_UserAuth.Models;

namespace Task4_UserAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .OrderByDescending(u => u.LastLogin)
                .ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Management()
        {
            var users = await _userManager.Users
                .OrderByDescending(u => u.LastLogin)
                .ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Block([FromBody] List<string> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                    await _userManager.ResetAccessFailedCountAsync(user);
                }
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Unblock([FromBody] List<string> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    await _userManager.ResetAccessFailedCountAsync(user);
                }
            }
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromBody] List<string> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            return Ok();
        }
    }
}
