namespace PackingListChatBot.Models
{
    /// <summary>
    /// Defines the PackingItem model
    /// Category: Category of the item
    /// Examples: List of example items in the category
    /// Reason: Explanation why the items are needed, to be used by the LLM
    /// </summary>
    public class PackingItem
    {
        public string Category { get; set; } = string.Empty;
        public List<string> Examples { get; set; } = new List<string>();
        public string? Reason { get; set; }
    }
}
