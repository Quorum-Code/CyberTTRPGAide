using CyberTTRPGAideWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
