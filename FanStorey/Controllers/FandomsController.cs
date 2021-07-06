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
    public class FandomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FandomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fandoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fandom.ToListAsync());
        }

        // GET: Fandoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fandom == null)
            {
                return NotFound();
            }

            return View(fandom);
        }

        // GET: Fandoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fandoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Fandom fandom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fandom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fandom);
        }

        // GET: Fandoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandom.FindAsync(id);
            if (fandom == null)
            {
                return NotFound();
            }
            return View(fandom);
        }

        // POST: Fandoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Fandom fandom)
        {
            if (id != fandom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fandom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FandomExists(fandom.Id))
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
            return View(fandom);
        }

        // GET: Fandoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fandom == null)
            {
                return NotFound();
            }

            return View(fandom);
        }

        // POST: Fandoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fandom = await _context.Fandom.FindAsync(id);
            _context.Fandom.Remove(fandom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FandomExists(int id)
        {
            return _context.Fandom.Any(e => e.Id == id);
        }
    }
}
