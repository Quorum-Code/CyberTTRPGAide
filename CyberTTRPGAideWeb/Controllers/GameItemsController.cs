using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberTTRPGAideWeb.Data;
using CyberTTRPGAideWeb.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CyberTTRPGAideWeb.Controllers
{
    public class GameItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameItem.ToListAsync());
        }

        // GET: GameItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameItem = await _context.GameItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameItem == null)
            {
                return NotFound();
            }

            return View(gameItem);
        }

        // GET: GameItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Create([Bind("Id,Name,Value,Description,Effects,Weight")] GameItem gameItem)
        {
            if (ModelState.IsValid)
            {
                gameItem.Id = Guid.NewGuid();
                _context.Add(gameItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameItem);
        }

        // GET: GameItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameItem = await _context.GameItem.FindAsync(id);
            if (gameItem == null)
            {
                return NotFound();
            }
            return View(gameItem);
        }

        // POST: GameItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Value,Description,Effects,Weight")] GameItem gameItem)
        {
            if (id != gameItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameItemExists(gameItem.Id))
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
            return View(gameItem);
        }

        // GET: GameItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameItem = await _context.GameItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameItem == null)
            {
                return NotFound();
            }

            return View(gameItem);
        }

        // POST: GameItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameItem = await _context.GameItem.FindAsync(id);
            if (gameItem != null)
            {
                _context.GameItem.Remove(gameItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameItemExists(Guid id)
        {
            return _context.GameItem.Any(e => e.Id == id);
        }
    }
}
