using PackingListChatBot.SemanticKernel.Prompts;

namespace PackingListChatBotTests.SemanticKernel.Mocks
{
    public class MockPromptProvider : IPromptProvider
    {
        public string GetPackingIntentPrompt()
        {
            return """
                   mockPrompt
                   """;
        }

        public string GetPackingListReadoutPrompt()
        {
            return "mockFilePath";
        }
    }
}
