using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CyberTTRPGAideWeb.Models.Entities
{
    public class AccessCode
    {
        [Key]
        public Guid Id { get; set; }
    }
}
