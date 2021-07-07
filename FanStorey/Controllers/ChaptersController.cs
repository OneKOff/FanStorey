using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FanStorey.Data;
using FanStorey.Models;

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

            var story = await _context.Story
                .FirstOrDefaultAsync(m => m.Id == id);
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

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChapterViewModel ccvm)
        {
            if (ModelState.IsValid)
            {
                ccvm.ChapterNew.PostDate = DateTime.Now;
                ccvm.ChapterNew.LastUpdateDate = DateTime.Now;

                _context.Add(ccvm.ChapterNew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), ccvm.StoryFrom.Id);
            }
            return View(ccvm.ChapterNew);
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
            return View(chapter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChapterViewModel ccvm)
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
                return View("Index", ccvm.StoryFrom);
            }
            return View(ccvm.ChapterNew);
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
            _context.Chapter.Remove(chapter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChapterExists(int id)
        {
            return _context.Chapter.Any(e => e.Id == id);
        }
    }
}
