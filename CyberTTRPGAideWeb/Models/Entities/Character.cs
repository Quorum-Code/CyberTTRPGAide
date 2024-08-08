using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberTTRPGAideWeb.Models.Entities
{
    public class Character
    {
        public Guid Id { get; set; }
        
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }

        public Character()
        {
            UserId = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
