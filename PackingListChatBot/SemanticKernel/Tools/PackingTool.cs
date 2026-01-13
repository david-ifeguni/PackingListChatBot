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
                if (packingIntentResult == null)
                {
                    return PoliteClarificationRequest();
                }

                packingIntentResult.ConfidenceScore = CalculateConfidenceScore(packingIntentResult);
                if (packingIntentResult.ConfidenceScore < 0.6)
                { 
                    packingIntentResult.NeedsClarification = true;
                    packingIntentResult.ClarificationQuestion = "Can you clarify a few details about your trip?";
                }
                return packingIntentResult;
            }
            catch
            {
                return PoliteClarificationRequest();
            }
        }

        private static double CalculateConfidenceScore(PackingIntentResult packingIntentResult)
        {
            double score = 1.0;
            
            if (string.IsNullOrEmpty(packingIntentResult.Location)) score -= 0.3;

            if (!packingIntentResult.StartTime.HasValue || !packingIntentResult.EndTime.HasValue) score -= 0.3;

            if (packingIntentResult.UnknownActivities != null && packingIntentResult.UnknownActivities.Count > 0) score -= 0.2;

            if (packingIntentResult.NeedsClarification) score -= 0.4;

            return Math.Max(0, score);
        }

        private static PackingIntentResult PoliteClarificationRequest()
        {
            return new PackingIntentResult
            {
                NeedsClarification = true,
                ClarificationQuestion = "I'm sorry, I don't fully understand your travel details. Can you please clarify your location, travel dates and any planned activities?"
            };
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
