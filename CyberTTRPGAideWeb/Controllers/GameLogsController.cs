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
    public class GameLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameLog.ToListAsync());
        }

        // GET: GameLogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameLog = await _context.GameLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLog == null)
            {
                return NotFound();
            }

            return View(gameLog);
        }

        // GET: GameLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Create([Bind("Id,CharacterSheetId,Title,Text,TimeCreatedAt")] GameLog gameLog)
        {
            if (ModelState.IsValid)
            {
                gameLog.Id = Guid.NewGuid();
                _context.Add(gameLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameLog);
        }

        // GET: GameLogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameLog = await _context.GameLog.FindAsync(id);
            if (gameLog == null)
            {
                return NotFound();
            }
            return View(gameLog);
        }

        // POST: GameLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CharacterSheetId,Title,Text,TimeCreatedAt")] GameLog gameLog)
        {
            if (id != gameLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameLogExists(gameLog.Id))
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
            return View(gameLog);
        }

        // GET: GameLogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameLog = await _context.GameLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLog == null)
            {
                return NotFound();
            }

            return View(gameLog);
        }

        // POST: GameLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameLog = await _context.GameLog.FindAsync(id);
            if (gameLog != null)
            {
                _context.GameLog.Remove(gameLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameLogExists(Guid id)
        {
            return _context.GameLog.Any(e => e.Id == id);
        }
    }
}
