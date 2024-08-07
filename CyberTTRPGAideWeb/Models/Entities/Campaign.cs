namespace CyberTTRPGAideWeb.Models.Entities
{
    public class Campaign
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Campaign()
        {
            Name = string.Empty;
        }
    }
}
