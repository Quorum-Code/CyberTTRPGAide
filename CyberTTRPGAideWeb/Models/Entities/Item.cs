namespace CyberTTRPGAideWeb.Models.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public string Effects { get; set; }
        public float Weight { get; set; }

        public Item()
        {
            Name = string.Empty;
            Description = string.Empty;
            Effects = string.Empty;
        }
    }
}
