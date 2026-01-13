using OpenAI.Responses;

namespace PackingListChatBot.SemanticKernel.Prompts
{
    public interface IPromptProvider
    {
        string GetPackingIntentPrompt();

        string GetPackingListReadoutPrompt();
    }
}
