using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microblog.Data;
using Microblog.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microblog.Models;
using Microsoft.AspNetCore.Identity;

namespace Microblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search, int interest, string all)
        {
            // Set up default query.
            var result = from p in _context.Post
                         select p;

            var model = new HomeViewModel();

            // Default title.
            model.Title = "Posts selected for you";

            if (!String.IsNullOrEmpty(search))
            {
                model.Title = "Search results";
                model.SearchTerm = search;

                // Search by username when search query starts with '@'.
                if (search[0] == '@')
                {
                    // Remove the @ in the beginning, otherwise search will not return anything.
                    search = search.Remove(0, 1);
                    result = result.Include(u => u.User).Where(s => s.User.UserName.Contains(search));
                }
                else
                    result = result.Where(s => s.Title.Contains(search));
            }

            // Get an orderd list of interests, and convert it to a selectlist for the dropdown menu.
            var interests = _context.Interest.OrderBy(c => c.Name).Select(x => new { Id = x.ID, Value = x.Name });
            model.InterestsList = new SelectList(interests, "Id", "Value");

            // Filter by interest if the user selected an interest other than "All".
            if (interest > 0 && interest <= interests.Count())
            {
                model.PostsList = await result.
                    Include(u => u.User).
                    Include(p => p.PostInterests).ThenInclude(s => s.Interest).
                    Where(p => p.Public == true).
                    Where(post => post.PostInterests.Any(i => i.InterestId == interest)).
                    ToListAsync();
            }
            else
            {
                ApplicationUser user = await GetCurrentUserAsync();
                if (user != null && String.IsNullOrEmpty(all)) // The all keyword is quick fix to prevent the user from not being able to view all posts.
                {
                    // Get a list of the user' interests.
                    user = await _context.Users.
                        Include(i => i.UserInterests).ThenInclude(s => s.Interest).
                        SingleAsync(i => i.Id == user.Id);

                    var iList = user.UserInterests.ToList();

                    // Look for overlap between the post interests and the user' interests.
                    model.PostsList = await result.
                        Include(u => u.User).
                        Include(p => p.PostInterests).ThenInclude(s => s.Interest).
                        Where(p => p.Public == true).
                        Where(post => post.PostInterests.Any(i => iList.Any(a => a.InterestId == i.InterestId))).
                        ToListAsync();
                }
                else
                {
                    model.Title = "All posts";
                    // By default we just display all posts.
                    model.PostsList = await result.
                    Include(u => u.User).
                    Include(p => p.PostInterests).ThenInclude(s => s.Interest).
                    Where(p => p.Public == true).
                    ToListAsync();
                }
            }

            return View(model);
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

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
