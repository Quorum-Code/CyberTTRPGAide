using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberTTRPGAideWeb.Data;
using CyberTTRPGAideWeb.Models.Entities;

namespace CyberTTRPGAideWeb.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inventories.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(Guid? characterSheetId, Guid? gameItemId)
        {
            if (characterSheetId == null || gameItemId == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(m => m.CharacterSheetId == characterSheetId && m.GameItemId == gameItemId);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterSheetId,GameItemId,ItemCount")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                inventory.CharacterSheetId = Guid.NewGuid();
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(Guid? characterSheetId, Guid? gameItemId)
        {
            if (characterSheetId == null || gameItemId == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(characterSheetId, gameItemId);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CharacterSheetId,GameItemId,ItemCount")] Inventory inventory)
        {
            if (id != inventory.CharacterSheetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.CharacterSheetId, inventory.GameItemId))
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
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(Guid? characterSheetId, Guid? gameItemId)
        {
            if (characterSheetId == null || gameItemId == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(m => m.CharacterSheetId == characterSheetId && m.GameItemId == gameItemId);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? characterSheetId, Guid? gameItemId)
        {
            var inventory = await _context.Inventories.FindAsync(characterSheetId, gameItemId);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(Guid? characterSheetId, Guid? gameItemId)
        {
            return _context.Inventories.Any(e => e.CharacterSheetId == characterSheetId && e.GameItemId == gameItemId);
        }
    }
}
