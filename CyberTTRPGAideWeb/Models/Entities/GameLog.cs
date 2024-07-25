using Microsoft.EntityFrameworkCore;

namespace CyberTTRPGAideWeb.Models.Entities
{
    [PrimaryKey("Id")]
    public class GameLog
    {
        public Guid Id { get; set; }
        public Guid CharacterSheetId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreatedAt { get; set; }

        public GameLog()
        {
            Title = string.Empty;
            Text = string.Empty;
        }
    }
}
