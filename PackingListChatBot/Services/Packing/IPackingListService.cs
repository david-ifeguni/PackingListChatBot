using PackingListChatBot.Models;

namespace PackingListChatBot.Services.Packing
{
    public interface IPackingListService
    {
        Task<PackingListServiceResponse> GeneratePackingListAsync(PackingChatRequest packingChatRequest, string conversationId);
    }
}
