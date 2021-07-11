﻿using System;
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
            return View(await _context.User.
                Include(m => m.Author).
                Include(m => m.Chapters).
                Include(m => m.StoryFandom).
                ToListAsync());
        }

        public async Task<IActionResult> CurrentUserStories()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return View("Index", await _context.User.
                Include(m => m.Author).
                Include(m => m.Chapters).
                Include(m => m.StoryFandom).
                Where(m => m.Author == currentUser).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.User.
                Include(m => m.Author).
                Include(m => m.Chapters).
                Include(m => m.StoryFandom).
                FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        [Authorize]
        public async Task<IActionResult> Create(string id)
        {
            List<Fandom> fandomList = await _context.Fandom.ToListAsync();
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            ViewBag.Fandoms = new SelectList(fandomList, "Id", "Name");
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string returnUrl, int FandomId, string userId, [Bind("Id,Title,Description")] Story story)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _context.ApplicationUser.FindAsync(userId);
                story.Author = user;
                story.PostDate = DateTime.Now;
                story.LastUpdateDate = DateTime.Now;
                story.StoryFandom = await _context.Fandom.FindAsync(FandomId);

                _context.Add(story);
                await _context.SaveChangesAsync();
                return Redirect(returnUrl);
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

            Story story = await _context.User.
                Include(m => m.Author).
                Include(m => m.Chapters).
                Include(m => m.StoryFandom).
                FirstOrDefaultAsync(m => m.Id == id);

            List<Fandom> fandomList = await _context.Fandom.ToListAsync();
            ViewBag.Fandoms = new SelectList(fandomList, "Id", "Name", story.StoryFandom.Id);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

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
        public async Task<IActionResult> Edit(int id, string returnUrl, int FandomId, [Bind("Id,Title,Description")] Story story)
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

            var story = await _context.User
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
            var story = await _context.User.FindAsync(id);
            _context.User.Remove(story);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StoryExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
