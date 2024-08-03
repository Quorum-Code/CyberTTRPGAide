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
        public async Task<IActionResult> DeleteConfirmed()
        {
            var before = _context.GameItem.Count();
            _context.GameItem.RemoveRange();
            var after = _context.GameItem.Count();

            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("GenerateGameItems")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateGameItemsConfirmed()
        {
            Console.WriteLine("Called Generate Items");

            //GameItem g = new();
            //g.Id = Guid.NewGuid();
            //_context.Add(g);
            //await _context.SaveChangesAsync();

            var path = "TestData/GameItem.json";

            if (System.IO.File.Exists(path))
            {
                using StreamReader reader = new(path);
                string text = reader.ReadToEnd();

                var items = JsonSerializer.Deserialize<GameItem>(text);

                Console.WriteLine(items);
            }
            else 
            {
                Console.WriteLine("File path: '" + path + "' does not exist");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
