namespace PackingListChatBot.Models
{
    public class TravelContext
    {
        public string? Location { get; set; }
        public DateOnly? StartTime { get; set; }
        public DateOnly? EndTime { get; set; }
        public HashSet<TravelActivities> Activities { get; set; } = new HashSet<TravelActivities>();
        public bool IsComplete => !string.IsNullOrEmpty(Location) && StartTime.HasValue && EndTime.HasValue;
    }
}
