namespace PackingListChatBot.Models
{
    public class PackingChatResponse
    {
        public bool NeedsClarification { get; set; }
        public string? ClarificationQuestion { get; set; }
        public PackingConstraints? PackingConstraints { get; set; }
    }
}
