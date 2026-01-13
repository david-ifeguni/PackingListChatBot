namespace PackingListChatBot.Models
{
    /// <summary>
    /// Response object to be returned by the PackingList Service
    /// </summary>
    public class PackingListServiceResponse
    {
        public bool NeedsClarification { get; set; }
        public string? ClarificationQuestion { get; set; }
        public string? Error { get; set; }
        public string? FormattedPackingList { get; set; }
        public bool IsSuccess => !NeedsClarification && Error == null;
    }
}
