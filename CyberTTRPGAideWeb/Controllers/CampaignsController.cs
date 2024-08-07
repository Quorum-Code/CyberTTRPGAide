using CyberTTRPGAideWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberTTRPGAideWeb.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CampaignsController(UserManager<IdentityUser> userManager, ApplicationDbContext context) 
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var campaigns = await _context.Campaign.ToListAsync();

            return View(campaigns);
        }
    }
}
