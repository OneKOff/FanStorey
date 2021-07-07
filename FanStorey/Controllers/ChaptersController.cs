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

            return View(await _context.Chapter.Where(m => m.StoryFrom == story).ToListAsync());
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

        public async Task<IActionResult> Create(int? id)
        {
            Chapter chapter = new Chapter();
            chapter.StoryFrom = await _context.Story.FindAsync(id);
            return View(chapter);
        }

        // POST: Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ChapterText,StoryFrom")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                chapter.PostDate = DateTime.Now;
                chapter.LastUpdateDate = DateTime.Now;

                _context.Add(chapter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chapter);
        }

        // GET: Chapters/Edit/5
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

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ChapterText")] Chapter chapter)
        {
            if (id != chapter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chapter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapterExists(chapter.Id))
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
            return View(chapter);
        }

        // GET: Chapters/Delete/5
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

        // POST: Chapters/Delete/5
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
