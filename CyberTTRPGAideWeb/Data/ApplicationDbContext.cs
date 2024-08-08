using CyberTTRPGAideWeb.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CyberTTRPGAideWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<CyberTTRPGAideWeb.Models.Entities.CharacterSheet> CharacterSheet { get; set; } = default!;
        public DbSet<CyberTTRPGAideWeb.Models.Entities.Character> Characters { get; set; } = default!;
        public DbSet<CyberTTRPGAideWeb.Models.Entities.GameItem> GameItem { get; set; } = default!;
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<CyberTTRPGAideWeb.Models.Entities.GameLog> GameLog { get; set; } = default!;
        public DbSet<CyberTTRPGAideWeb.Models.Entities.Campaign> Campaign { get; set; } = default!;
    }
}
