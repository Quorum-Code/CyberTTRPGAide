using CyberTTRPGAideWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using CyberTTRPGAideWeb.Models.Entities;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;

namespace CyberTTRPGAideWeb.Controllers
{
    public class DebugController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DebugController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DebugController
        [Authorize(Roles = "Developer")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> DeleteConfirmed()
        {
            _context.GameItem.RemoveRange();
            await _context.SaveChangesAsync();

            TempData["status"] = "Successfully deleted all GameItems.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("GenerateGameItems")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> GenerateGameItemsConfirmed()
        {
            var path = "TestData/GameItems.json";
            if (!System.IO.File.Exists(path)) 
            {
                TempData["status"] = "File path: '" + path + "' does not exist";
                return RedirectToAction(nameof(Index));
            }

            using StreamReader reader = new(path);
            string text = reader.ReadToEnd();

            var items = JsonSerializer.Deserialize<List<Item>>(text);

            if (items == null) 
            {
                TempData["status"] = "Failed to create GameItems.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var item in items)
            {
                if (await _context.GameItem.FirstOrDefaultAsync(m => m.Id == item.Id) != null)
                    continue;

                _context.Add(item);
            }
            await _context.SaveChangesAsync();

            TempData["status"] = "Successfully created GameItems.";
            return RedirectToAction(nameof(Index));
        }
    }
}
