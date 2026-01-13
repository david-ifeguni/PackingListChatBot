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
        public bool NeedsClarification { get; set; }
        public string? ClarificationQuestion { get; set; }
        public double ConfidenceScore { get; set; }
        //public bool IsValid()
        //{
        //    return !string.IsNullOrEmpty(Location) && StartTime.HasValue && EndTime.HasValue && !NeedsClarification;
        //}
    }
}
