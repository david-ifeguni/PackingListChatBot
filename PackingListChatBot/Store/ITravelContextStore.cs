using PackingListChatBot.Models;

namespace PackingListChatBot.Store
{
    public interface ITravelContextStore
    {
        TravelContext Get(string conversationId);
        void Save(string conversationId, TravelContext context);
        void Clear(string conversationId);
    }
}
