using PackingListChatBot.Models;

namespace PackingListChatBot.Services
{
    public interface IPackingListService
    {
        Task<PackingListServiceResponse> GeneratePackingListAsync(PackingChatRequest packingChatRequest, string conversationId);
    }
}
