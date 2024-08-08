using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberTTRPGAideWeb.Data;
using CyberTTRPGAideWeb.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CyberTTRPGAideWeb.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CharactersController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Character
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var userId = user?.Id;

            var myCharacters = await _context.Characters.Where(c => c.UserId == userId).ToListAsync();

            return View(myCharacters);
        }

        // GET: Character/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CharacterSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,Level")] Character character)
        {
            if (ModelState.IsValid)
            {
                character.Id = Guid.NewGuid();
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(character);
        }

        // GET: Character/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return View(character);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,Name,Level")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
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
            return View(character);
        }

        // GET: CharacterSheets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // POST: CharacterSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character != null)
            {
                _context.Characters.Remove(character);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(Guid id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
