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
        // TODO: Remove the below parameters as they are no longer needed due to TravelContext
        public bool NeedsClarification { get; set; }
        public string? ClarificationQuestion { get; set; }
        public double ConfidenceScore { get; set; }
    }
}
