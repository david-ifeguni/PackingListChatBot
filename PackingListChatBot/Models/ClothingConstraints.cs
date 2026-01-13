namespace PackingListChatBot.Models
{
    /// <summary>
    /// Defines the Clothing Rules based off the weather
    /// </summary>
    public class ClothingConstraints
    {
        public bool NeedsWarmLayers { get; set; }
        public bool NeedsRainProtection { get; set; }
        public bool NeedsBreathableClothing { get; set; }
        public bool NeedsSunProtection { get; set; }
        public bool LargeTempSwing { get; set; }
        public List<string> Notes { get; set; } = new List<string>();
    }
}
