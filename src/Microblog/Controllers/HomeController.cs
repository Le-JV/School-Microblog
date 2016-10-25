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

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            // Set up default query.
            var result = from p in _context.Post
                         select p;

            ViewData["Header"] = "Recent posts";

            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(s => s.Title.Contains(search));
                ViewData["Header"] = "Search results";
            }

            return View(await result.Include(u => u.User).Where(p => p.Public == true).ToListAsync());
        }

        // GET: Posts/Search
        /*public async Task<IActionResult> (string search)
        {
            if(String.IsNullOrEmpty(search))
                return View("Error");

            return View(await _context.Post.Where(s => s.Title.Contains(search)).ToListAsync());
        }*/

        public IActionResult Error()
        {
            return View();
        }
    }
}
