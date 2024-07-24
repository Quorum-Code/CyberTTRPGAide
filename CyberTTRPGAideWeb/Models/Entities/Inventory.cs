using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberTTRPGAideWeb.Models.Entities
{
    [PrimaryKey(nameof(CharacterSheetId), nameof(GameItemId))]
    public class Inventory
    {
        [ForeignKey("CharacterSheet")]
        public Guid CharacterSheetId { get; set; }
        [ForeignKey("GameItem")]
        public Guid GameItemId { get; set; }
        public int ItemCount { get; set; }
    }
}
