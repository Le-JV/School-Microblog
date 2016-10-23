using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microblog.Data;

namespace Microblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Only display posts that are public.
            return View(await _context.Post.Include(u => u.User).Where(p => p.Public == true).ToListAsync());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
