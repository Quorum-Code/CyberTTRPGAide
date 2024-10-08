﻿using System;
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
    public class CharacterSheetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterSheetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CharacterSheets
        public async Task<IActionResult> Index()
        {
            return View(await _context.CharacterSheet.ToListAsync());
        }

        // GET: CharacterSheets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterSheet = await _context.CharacterSheet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterSheet == null)
            {
                return NotFound();
            }

            return View(characterSheet);
        }

        // GET: CharacterSheets/Create
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
        public async Task<IActionResult> Create([Bind("Id,UserId,CharacterName,Level")] CharacterSheet characterSheet)
        {
            if (ModelState.IsValid)
            {
                characterSheet.Id = Guid.NewGuid();
                _context.Add(characterSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(characterSheet);
        }

        // GET: CharacterSheets/Edit/5
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterSheet = await _context.CharacterSheet.FindAsync(id);
            if (characterSheet == null)
            {
                return NotFound();
            }
            return View(characterSheet);
        }

        // POST: CharacterSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,CharacterName,Level")] CharacterSheet characterSheet)
        {
            if (id != characterSheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characterSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterSheetExists(characterSheet.Id))
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
            return View(characterSheet);
        }

        // GET: CharacterSheets/Delete/5
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterSheet = await _context.CharacterSheet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterSheet == null)
            {
                return NotFound();
            }

            return View(characterSheet);
        }

        // POST: CharacterSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Verified")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var characterSheet = await _context.CharacterSheet.FindAsync(id);
            if (characterSheet != null)
            {
                _context.CharacterSheet.Remove(characterSheet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterSheetExists(Guid id)
        {
            return _context.CharacterSheet.Any(e => e.Id == id);
        }
    }
}
