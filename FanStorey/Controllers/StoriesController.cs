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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public StoriesController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Story.
                Include(m => m.Author).
                Include(m => m.Chapters).
                Include(m => m.StoryFandom).
                ToListAsync());
        }

        public async Task<IActionResult> CurrentUserStories()
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return View("Index", await _context.Story.Where(m => m.Author == currentUser).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
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

        [Authorize]
        public async Task<IActionResult> Create()
        {
            List<Fandom> fandomList = await _context.Fandom.ToListAsync();
            ViewBag.Fandoms = new SelectList(fandomList, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id","Title","Description")] Story story, int FandomId)
        {
            if (ModelState.IsValid)
            {
                IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                story.Author = currentUser;
                story.PostDate = DateTime.Now;
                story.LastUpdateDate = DateTime.Now;
                story.StoryFandom = await _context.Fandom.FindAsync(FandomId);

                _context.Add(story);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(story);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Fandom> fandomList = await _context.Fandom.ToListAsync();
            ViewBag.Fandoms = new SelectList(fandomList, "Id", "Name");

            var story = await _context.Story.
                Include(m => m.Author).
                Include(m => m.Chapters).
                Include(m => m.StoryFandom).
                FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }
            if (story.Author == await _userManager.GetUserAsync(HttpContext.User))
            {
                return View(story);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Story story, int FandomId)
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
                    story.StoryFandom = await _context.Fandom.FindAsync(FandomId);

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
                return RedirectToAction("Index");
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
