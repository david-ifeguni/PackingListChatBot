using PackingListChatBot.Models;

namespace PackingListChatBot.Store
{
    public class TravelContextStore : ITravelContextStore
    {
        public Dictionary<string, TravelContext> store = new Dictionary<string, TravelContext>();
        public TravelContext Get(string conversationId)
        {
            if (store.TryGetValue(conversationId, out TravelContext travelContext)) return travelContext;
            else
            {
                travelContext = new TravelContext();
                store[conversationId] = travelContext;
                return travelContext;
            }
        }
        
        public void Save(string conversationId, TravelContext travelContext)
        {
            store[conversationId] = travelContext;
        }

        public void Clear(string conversationId)
        {
            store.Remove(conversationId);
        }
    }
}
