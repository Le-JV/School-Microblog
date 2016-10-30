using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microblog.Data;
using Microsoft.AspNetCore.Identity;
using Microblog.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Microblog.Controllers
{
    // Everything in this controller should only be accessible by members of the admin group.
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: Admin/Posts/5
        public async Task<IActionResult> Posts(string Id)
        {
            // Empty string means no user was specified, in which case we return out and display all posts.
            if(String.IsNullOrWhiteSpace(Id))
                return View(await _context.Post.Include(u => u.User).ToListAsync());

            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                return View(_context.Post.Include(u => u.User).Where(p => p.User == user));
            }

            return NotFound();
        }

        // POST: Admin/Promote/5
        [HttpPost, ActionName("Promote")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Promote(string Id)
        {
            if (String.IsNullOrWhiteSpace(Id))
                return NotFound();

            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("Users");
            }

            return NotFound();
        }
    }
}