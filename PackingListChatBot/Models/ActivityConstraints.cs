namespace PackingListChatBot.Models
{
    /// <summary>
    /// Defines the Activity Rules based off the weather
    /// </summary>
    public class ActivityConstraints
    {
        public bool NeedsComfortableWalkingFootwear { get; set; }
        public bool NeedsWaterProofFootwear { get; set; }
        public bool NeedsFormalWear { get; set; }
        public bool NeedsSwimwear { get; set; }
        public bool NeedsAthleticWear { get; set; }
        public bool NeedsChangeOfClothes { get; set; }
        public bool NeedsQuickDryClothing { get; set; }
        public bool NeedsWeatherFlexibleWear { get; set; }
        public List<string> Notes { get; set; } = new List<string>();
    }
}
