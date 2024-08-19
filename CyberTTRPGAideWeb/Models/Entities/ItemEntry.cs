namespace CyberTTRPGAideWeb.Models.Entities
{
    public class ItemEntry
    {
        public int Id { get; set; }
        public virtual Character Character { get; set; }
        public virtual Item Item { get; set; }
        public int Count { get; set; }
    }
}
