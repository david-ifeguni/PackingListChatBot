namespace PackingListChatBot.Models
{
    /// <summary>
    /// Defines the PackingItem model
    /// Item: The name of the item
    /// Reason: Reason the item is needed
    /// </summary>
    public class PackingItem
    {
        // public string? Item {  get; set; }
        public string Category { get; set; } = string.Empty;
        public List<string> Examples { get; set; } = new List<string>();
        public string? Reason { get; set; }
    }
}
