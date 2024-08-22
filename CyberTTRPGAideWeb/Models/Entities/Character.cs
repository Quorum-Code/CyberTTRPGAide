using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ItemEntry>? ItemEntries { get; set; }

        public Character()
        {
            UserId = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
