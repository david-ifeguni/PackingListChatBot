using System.Text.Json;
using PackingListChatBot.Models;
using PackingListChatBot.Packing;
using PackingListChatBot.Helpers;
using PackingListChatBot.SemanticKernel.KernelFactory;
using PackingListChatBot.SemanticKernel.Prompts;
using PackingListChatBot.SemanticKernel.Tools;
using PackingListChatBot.Store;

namespace PackingListChatBot.Services
{
    public class PackingListService : IPackingListService
    {
        private readonly IWeatherService weatherService;
        private readonly IPackingTool packingTool;
        private readonly IPackingListGenerator packingListGenerator;
        private readonly IPromptProvider promptProvider;
        private readonly IKernelInvoker kernelInvoker;
        private readonly ITravelContextStore travelContextStore;
        private readonly PackingConstraintsAggregator packingConstraintsAggregator;

        public PackingListService(IWeatherService weatherService,
            IPackingTool packingTool,
            IPackingListGenerator packingListGenerator,
            IPromptProvider promptProvider,
            IKernelInvoker kernelInvoker,
            ITravelContextStore travelContextStore,
            PackingConstraintsAggregator packingConstraintsAggregator)
        {
            this.weatherService = weatherService;
            this.packingTool = packingTool;
            this.packingListGenerator = packingListGenerator;
            this.promptProvider = promptProvider;
            this.kernelInvoker = kernelInvoker;
            this.travelContextStore = travelContextStore;
            this.packingConstraintsAggregator = packingConstraintsAggregator;
        }

        public async Task<PackingListServiceResponse> GeneratePackingListAsync(PackingChatRequest packingChatRequest, string conversationId)
        {
            // Call the LLM (via the Orchestrator) to get the PackingIntentResult
            var packingIntentResult = await packingTool.GetPackingIntentAsync(packingChatRequest.Message);

            // Get current travel context
            var travelContext = travelContextStore.Get(conversationId);

            // Update and save the travel context
            TravelContextHelper.Update(travelContext, packingIntentResult);
            travelContextStore.Save(conversationId, travelContext);

            if (!travelContext.IsComplete)
            {
                // Decide which info is missing
                // Set the clarification question accordingly
                // Return new PackingListServiceResponse
                // string question = TravelContextHelper.GetClarificationQuestion(travelContext);
                return new PackingListServiceResponse
                {
                    NeedsClarification = true,
                    ClarificationQuestion = TravelContextHelper.GetClarificationQuestion(travelContext),
                };
            }

            //// If clarification is needed, return immediately with clarification question
            //if (packingIntentResult.NeedsClarification)
            //{
            //    return new PackingListServiceResponse
            //    {
            //        NeedsClarification = true,
            //        ClarificationQuestion = packingIntentResult.ClarificationQuestion ?? "Temporary clarification question",
            //    };
            //}

            //if (string.IsNullOrEmpty(packingIntentResult.Location))
            //{
            //    return new PackingListServiceResponse
            //    {
            //        NeedsClarification = true,
            //        ClarificationQuestion = "Could you clarify what your travel destination is?",
            //    };
            //}

            //if (!packingIntentResult.StartTime.HasValue || !packingIntentResult.EndTime.HasValue)
            //{
            //    return new PackingListServiceResponse
            //    {
            //        NeedsClarification = true,
            //        ClarificationQuestion = "Could you clarify your travel dates?",
            //    };
            //}

            // Use packingIntentResult to get the Weather Profile from the Weather Service (powered by Open-Meteo APIs)
            var weatherProfile = await weatherService.GetWeatherProfile(travelContext.Location, travelContext.StartTime.Value, travelContext.EndTime.Value);

            // Use weatherProfile and activities from packingIntentResult to get the packingConstraints
            var packingConstraints = packingConstraintsAggregator.BuildPackingConstraints(weatherProfile, travelContext.Activities.ToList());
            // TODO: Fix the warning above, ideally shouldn't need activities to be mandatory in order to generate a packing list

            // Use the packingConstraints to build the packing list
            var packingList = packingListGenerator.GeneratePackingList(
                packingConstraints,
                travelContext.Location,
                travelContext.StartTime.Value,
                travelContext.EndTime.Value,
                travelContext.Activities.ToList());

            var readoutPrompt = promptProvider.GetPackingListReadoutPrompt();

            var parameters = new Dictionary<string, object>
            {
                ["location"] = travelContext.Location,
                ["startTime"] = $"{travelContext.StartTime:yyyy-MM-dd}",
                ["endTime"] = $"{travelContext.EndTime:yyyy-MM-dd}",
                ["activities"] = string.Join(", ", travelContext.Activities.ToList()),
                ["packingListJson"] = JsonSerializer.Serialize(packingList)
            };

            string message;

            try
            {
                message = await kernelInvoker.InvokeAsync(readoutPrompt, parameters);
            }
            catch (Exception ex)
            {
                return new PackingListServiceResponse
                {
                    Error = $"LLM failed to format response: {ex.Message}",
                };
            }

            return new PackingListServiceResponse
            {
                FormattedPackingList = message,
            };
        }
    }
}
