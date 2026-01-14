using System.Text.Json;
using PackingListChatBot.Models;
using PackingListChatBot.Helpers;
using PackingListChatBot.SemanticKernel.KernelFactory;
using PackingListChatBot.SemanticKernel.Prompts;
using PackingListChatBot.SemanticKernel.Tools;
using PackingListChatBot.Store;
using PackingListChatBot.Services.OpenMeteoService;

namespace PackingListChatBot.Services.Packing
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
                // Return new PackingListServiceResonse with the correct clarification question
                return new PackingListServiceResponse
                {
                    NeedsClarification = true,
                    ClarificationQuestion = TravelContextHelper.GetClarificationQuestion(travelContext),
                };
            }

            // Use packingIntentResult to get the Weather Profile from the Weather Service (powered by Open-Meteo APIs)
            var weatherProfile = await weatherService.GetWeatherProfile(travelContext.Location, travelContext.StartTime.Value, travelContext.EndTime.Value);

            // Use weatherProfile and activities from packingIntentResult to get the packingConstraints
            var activityList = travelContext.Activities.ToList();
            var packingConstraints = packingConstraintsAggregator.BuildPackingConstraints(weatherProfile, activityList);

            // Use the packingConstraints to build the packing list
            var packingList = packingListGenerator.GeneratePackingList(
                packingConstraints,
                travelContext.Location,
                travelContext.StartTime.Value,
                travelContext.EndTime.Value,
                activityList);

            string message;
            try
            {
                message = await kernelInvoker.InvokeAsync(
                    promptProvider.GetPackingListReadoutPrompt(), 
                    ParamBuilder.BuildPackingListReadoutParams(weatherProfile, packingList));
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
