using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microblog.Data;
using Microblog.Models;
using Microsoft.AspNetCore.Identity;
using Microblog.Models.PostViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microblog.Controllers
{
    // ASP Core & EF conventions seem to do a lot of the CRUD work on the controller level instead of in a model.
    // https://docs.asp.net/en/latest/data/ef-mvc/crud.html
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PostsController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;    
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            // Get list of my posts.
            var user = await GetCurrentUserAsync();
            return View(_context.Post.
                Include(p => p.PostInterests).ThenInclude(s => s.Interest).
                Where(m => m.User == user));
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // No lazy loading in EF7 yet, use eager loading by using Include manually.
            var post = await _context.Post.
                Include(u => u.User).
                Include(c => c.Comments).ThenInclude(ca => ca.User).
                Include(p => p.PostInterests).ThenInclude(s => s.Interest).
                SingleOrDefaultAsync(m => m.ID == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            var interests = _context.Interest.OrderBy(c => c.Name).Select(x => new { Id = x.ID, Value = x.Name });

            var model = new PostViewModel()
            {
                InterestsList = new SelectList(interests, "Id", "Value")
            };
            return View(model);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            Post post = new Post();
            if (ModelState.IsValid)
            {
                post.Title = postViewModel.Title;
                post.Content = postViewModel.Content;
                post.Public = postViewModel.Public;

                // Amazingly, entity framework automatically makes the translation from ApplicationUser to it's UserID.
                post.User = await GetCurrentUserAsync();

                // Current DateTime on the machine (server).
                post.PostDate = DateTime.Now;

                // Generate excerpt of the content to show in the overview list.
                post.Excerpt = post.Content.Substring(0, (post.Content.Length < 144 ? post.Content.Length : 144));

                _context.Add(post);
                await _context.SaveChangesAsync();

                foreach (var i in postViewModel.InterestIds)
                {
                    var pi = new PostInterests { InterestId = i, PostId = post.ID };
                    _context.PostInterests.Add(pi);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Content,PostDate,Title")] Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.SingleOrDefaultAsync(m => m.ID == id);
            _context.Comment.RemoveRange(_context.Comment.Where(p => p.Post == post)); // Remove comments first, to avoid FK constraint error.
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Posts/Toggle/5
        [HttpPost, ActionName("Toggle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int id)
        {
            var post = await _context.Post.SingleOrDefaultAsync(m => m.ID == id);
            post.Public = !post.Public;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.ID == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
