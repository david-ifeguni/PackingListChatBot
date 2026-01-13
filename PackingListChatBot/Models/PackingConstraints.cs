namespace PackingListChatBot.Models
{
    /// <summary>
    /// The combined constraints lists
    /// </summary>
    public class PackingConstraints
    {
        public ClothingConstraints Clothing { get; set; } = new ClothingConstraints();
        public ActivityConstraints Activities { get; set; } = new ActivityConstraints();
    }
}
