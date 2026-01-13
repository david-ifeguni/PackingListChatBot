namespace PackingListChatBot.Models
{
    public class PackingList
    {
        public string Location { get; set; } = string.Empty;
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }
        public List<TravelActivities> Activities { get; set; } = new List<TravelActivities>();
        public List<PackingItem> PackingItems { get; set; } = new List<PackingItem>();
    }
}
