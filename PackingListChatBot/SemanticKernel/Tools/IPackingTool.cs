using PackingListChatBot.SemanticKernel.Models;

namespace PackingListChatBot.SemanticKernel.Tools
{
    public interface IPackingTool
    {
        Task<PackingIntentResult> GetPackingIntentAsync(string userMessage);
    }
}
