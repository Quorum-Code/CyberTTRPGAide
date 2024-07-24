using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberTTRPGAideWeb.Models.Entities
{
	public class CharacterSheet
	{
        public Guid Id { get; set; }

		[ForeignKey("AspNetUsers")]
		public string UserId { get; set; }
		public virtual IdentityUser User { get; set; }

		public string CharacterName { get; set; }
        public int Level { get; set; }

		public CharacterSheet()
		{

		}
    }
}
