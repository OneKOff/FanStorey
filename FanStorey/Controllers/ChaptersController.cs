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

namespace FanStorey.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChaptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story.                
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

        public async Task<IActionResult> Read(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        public IActionResult Create(int? id)
        {
            ChapterCreateViewModel ccvm = new ChapterCreateViewModel();
            ccvm.StoryId = id;
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            return View(ccvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string returnUrl, ChapterCreateViewModel ccvm)
        {
            if (ModelState.IsValid)
            {
                Story story = await _context.Story.
                    Include(m => m.Author).
                    Include(m => m.Chapters).
                    Include(m => m.StoryFandom).
                    FirstOrDefaultAsync(m => m.Id == ccvm.StoryId);

                ccvm.ChapterNew.PostDate = DateTime.Now;
                ccvm.ChapterNew.LastUpdateDate = DateTime.Now;
                story.LastUpdateDate = DateTime.Now;
                story.Chapters.Add(ccvm.ChapterNew);

                _context.Add(ccvm.ChapterNew);
                _context.Update(story);
                await _context.SaveChangesAsync();

                return Redirect(returnUrl);
            }
            return View(ccvm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapter.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            ChapterCreateViewModel ccvm = new ChapterCreateViewModel();
            ccvm.ChapterNew = chapter;
            Story story = await _context.Story.
                Include(m => m.Author).
                Include(m => m.Chapters).
                Include(m => m.StoryFandom).
                FirstOrDefaultAsync(m => m.Chapters.Contains(chapter));
            ccvm.StoryId = story.Id;
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            return View(ccvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string returnUrl, ChapterCreateViewModel ccvm)
        {
            if (id != ccvm.ChapterNew.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ccvm.ChapterNew.LastUpdateDate = DateTime.Now;
                    _context.Update(ccvm.ChapterNew);

                    Story story = await _context.Story.
                        Include(m => m.Author).
                        Include(m => m.Chapters).
                        Include(m => m.StoryFandom).
                        FirstOrDefaultAsync(m => m.Id == ccvm.StoryId);
                    story.LastUpdateDate = DateTime.Now;
                    _context.Update(story);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapterExists(ccvm.ChapterNew.Id))
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
            return View(ccvm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chapter = await _context.Chapter.FindAsync(id);
            var story = await _context.Story.Include(m => m.Chapters).
                FirstOrDefaultAsync(m => m.Chapters.Contains(chapter));

            story.LastUpdateDate = DateTime.Now;

            _context.Chapter.Remove(chapter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { story?.Id });
        }

        private bool ChapterExists(int id)
        {
            return _context.Chapter.Any(e => e.Id == id);
        }
    }
}
