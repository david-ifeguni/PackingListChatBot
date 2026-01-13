namespace PackingListChatBot.SemanticKernel.Prompts
{
    public class FilePromptProvider : IPromptProvider
    {
        public string GetPackingIntentPrompt()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SemanticKernel", "Prompts", "PackingIntentPrompt.txt");

            return File.ReadAllText(path);
        }

        public string GetPackingListReadoutPrompt()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SemanticKernel", "Prompts", "PackingListReadoutPrompt.txt");

            return File.ReadAllText(path);
        }
    }
}
