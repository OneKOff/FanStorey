using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FanStorey.Data;
using FanStorey.Models;
using FanStorey.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FanStorey.Controllers
{
    public class StoriesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public StoriesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Story.
                Include(m => m.User).
                Include(m => m.Chapters).
                Include(m => m.Fandom).
                ToListAsync());
        }

        public async Task<IActionResult> CurrentUserStories()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return View("Index", await _context.Story.
                Include(m => m.User).
                Include(m => m.Chapters).
                Include(m => m.Fandom).
                Where(m => m.User == currentUser).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story.
                Include(m => m.User).
                Include(m => m.Chapters).
                Include(m => m.Fandom).
                FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        [Authorize]
        public IActionResult Create(string id)
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (Fandom fandom in _context.Fandom)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = fandom.Name,
                    Value = fandom.Id.ToString()
                };
                selectListItems.Add(selectListItem);
            }
            ViewData["FandomId"] = selectListItems;
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string returnUrl, Story story)
        {
            ApplicationUser user = await _context.ApplicationUser.FindAsync(story.UserId);
            story.User = user;
            story.PostDate = DateTime.Now;
            story.LastUpdateDate = DateTime.Now;
            story.Fandom = await _context.Fandom.FindAsync(story.FandomId);

            _context.Add(story);
            await _context.SaveChangesAsync();
            return Redirect(returnUrl);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Story story = await _context.Story.
                FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (Fandom fandom in _context.Fandom)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = fandom.Name,
                    Value = fandom.Id.ToString(),
                    Selected = fandom.Id == story.FandomId ? true : false 
                };
                selectListItems.Add(selectListItem);
            }
            ViewData["FandomId"] = selectListItems;
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            ViewBag.UserId = story.UserId;

            return View(story);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Story story, string returnUrl)
        {
            if (id != story.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    story.LastUpdateDate = DateTime.Now;

                    _context.Update(story);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryExists(story.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(returnUrl);
            }

            return View(story);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story
                .FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var story = await _context.Story.FindAsync(id);
            _context.Story.Remove(story);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StoryExists(int id)
        {
            return _context.Story.Any(e => e.Id == id);
        }
    }
}
