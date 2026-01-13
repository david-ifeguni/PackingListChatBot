namespace PackingListChatBot.Models
{
    public class PackingListServiceResponse
    {
        public bool NeedsClarification { get; set; }
        public string? ClarificationQuestion { get; set; }
        public string? Error { get; set; }
        public string? FormattedPackingList { get; set; }
        public bool IsSuccess => !NeedsClarification && Error == null;
    }
}
