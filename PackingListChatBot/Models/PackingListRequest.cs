namespace PackingListChatBot.Models
{
    public class PackingListRequest
    {
        public string Location { get; set; } = string.Empty;
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }
        public List<string> Activities { get; set; } = new List<string>();
        public PackingList PackingList { get; set; } = new PackingList();
    }
}
