using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FanStorey.Data;
using FanStorey.Models;
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
            return View(await _context.Story.ToListAsync());
        }

        public async Task<IActionResult> UserStories()
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
        public async Task<IActionResult> Create(StoryViewModel csvm)
        {
            if (ModelState.IsValid)
            {
                // If admin, then Author is user which is used by admin
                //IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                //var userRole = await _context.UserRoles.FindAsync(currentUser);
                //story.Author = currentUser;
                csvm.StoryNew.StoryFandom = csvm.FandomSelected;

                _context.Add(csvm.StoryNew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(csvm.StoryNew);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }
            /*if (story.Author == await _userManager.GetUserAsync(HttpContext.User))
            {
                return View(story);
            }*/
            return View(story);
            //return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Story story)
        {
            if (id != story.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool StoryExists(int id)
        {
            return _context.Story.Any(e => e.Id == id);
        }
    }
}
