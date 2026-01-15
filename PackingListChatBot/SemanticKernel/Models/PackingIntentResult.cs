using PackingListChatBot.Models;

namespace PackingListChatBot.SemanticKernel.Models
{
    public class PackingIntentResult
    {
        public string? Location { get; set; }
        public DateOnly? StartTime { get; set; }
        public DateOnly? EndTime { get; set; }
        public List<TravelActivities>? Activities { get; set; }
        public List<string>? UnknownActivities { get; set; }
    }
}
