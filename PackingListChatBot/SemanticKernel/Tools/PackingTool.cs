using PackingListChatBot.SemanticKernel.Models;
using System.Text.Json;
using PackingListChatBot.SemanticKernel.KernelFactory;
using PackingListChatBot.SemanticKernel.Prompts;
using System.Text.Json.Serialization;

namespace PackingListChatBot.SemanticKernel.Tools
{
    /// <summary>
    /// Extracts parameters from the user's prompt to build the JSON for the PackingRequest
    /// </summary>
    public class PackingTool : IPackingTool
    {
        private readonly IKernelInvoker kernelInvoker;
        private readonly IPromptProvider promptProvider;

        public PackingTool(IKernelInvoker kernelInvoker, IPromptProvider promptProvider)
        {
            this.kernelInvoker = kernelInvoker;
            this.promptProvider = promptProvider;
        }

        /// <summary>
        /// Takes a user's message and returns the PackingIntentResult object
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns></returns>
        public async Task<PackingIntentResult> GetPackingIntentAsync(string userMessage)
        {
            string prompt = promptProvider.GetPackingIntentPrompt();

            var result = await kernelInvoker.InvokeAsync(prompt, new()
            {
                ["userMessage"] = userMessage,
                ["today"] = $"{DateTime.UtcNow:yyyy-MM-dd}"
            });

            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                    }
                };
                var json = ExtractJson(result.ToString());
                var packingIntentResult  = JsonSerializer.Deserialize<PackingIntentResult>(json, options);
                return packingIntentResult;
            }
            catch
            {
                return new PackingIntentResult();
            }
        }

        private static string ExtractJson(string rawResult)
        {
            int start = rawResult.IndexOf('{');
            int end = rawResult.LastIndexOf('}');

            if (start == -1 || end == -1 || end <= start)
            {
                throw new JsonException("Invalid JSON object in LLM response");
            }

            return rawResult.Substring(start, end - start + 1);
        }
    }
}
