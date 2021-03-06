using FanStorey.Data;
using FanStorey.Models;
using FanStorey.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanStorey.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Admin = (user != null && user.Admin);
            return View(await _context.ApplicationUser.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> CurrentUserPage()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                return NotFound();
            }
            UserPageViewModel upvm = new UserPageViewModel(currentUser, await _context.Story.
                Include(m => m.Chapters).
                Include(m => m.Fandom).
                Where(m => m.User == currentUser).
                ToListAsync(),
                true);
            return View("UserPage", upvm);
        }
        [Authorize]
        public async Task<IActionResult> UserPage(string id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ApplicationUser pageUser = await _context.ApplicationUser.
                FirstOrDefaultAsync(m => m.Id == id);
            if (currentUser == null || pageUser == null)
            {
                return NotFound();
            }
            UserPageViewModel upvm = new UserPageViewModel(pageUser, await _context.Story.
                Include(m => m.Chapters).
                Include(m => m.Fandom).
                Where(m => m.User == pageUser).
                ToListAsync(), 
                (currentUser != null && (currentUser.Id == id || currentUser.Admin)));
            return View(upvm);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUser.
                FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.ApplicationUser.
                FirstOrDefaultAsync(m => m.Id == id);
            bool deletedSelf = ( user == await _userManager.GetUserAsync(HttpContext.User) );
            List<Story> userStories = await _context.Story.
                Include(m => m.User).
                Include(m => m.Chapters).
                Include(m => m.Fandom).
                Where(m => m.User == user).ToListAsync();

            foreach (Story story in userStories)
            {
                _context.Story.Remove(story);
            }
            _context.ApplicationUser.Remove(user);
            await _context.SaveChangesAsync();

            if (deletedSelf)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Block(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUser.
                FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Blocked)           
                user.LockoutEnd = DateTimeOffset.Now;
            else            
                user.LockoutEnd = DateTimeOffset.MaxValue;            
            user.Blocked = !user.Blocked;
            _context.ApplicationUser.Update(user);
            await _context.SaveChangesAsync();

            if (user == await _userManager.GetUserAsync(HttpContext.User))
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> SwitchAdmin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Admin = !user.Admin;
            _context.ApplicationUser.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
