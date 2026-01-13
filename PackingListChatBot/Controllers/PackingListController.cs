using System.Text.Json;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PackingListChatBot.Models;
using PackingListChatBot.Packing;
using PackingListChatBot.SemanticKernel.KernelFactory;
using PackingListChatBot.SemanticKernel.Prompts;
using PackingListChatBot.SemanticKernel.Tools;
using PackingListChatBot.Services;

namespace PackingListChatBot.Controllers
{
    [ApiController]
    [Route("api/packing")]
    public class PackingListController : ControllerBase
    {
        //private readonly IWeatherService weatherService;
        //private readonly IPackingTool packingTool;
        //private readonly IPackingListGenerator packingListGenerator;  
        //private readonly IPromptProvider promptProvider;
        //private readonly IKernelInvoker kernelInvoker;
        private readonly IPackingListService packingListService;
        //private readonly PackingConstraintsAggregator packingConstraintsAggregator;

        public PackingListController(
            //IWeatherService weatherService, 
            //IPackingTool packingTool, 
            //IPackingListGenerator packingListGenerator,
            //IPromptProvider promptProvider,
            //IKernelInvoker kernelInvoker,
            IPackingListService packingListService
/*            PackingConstraintsAggregator packingConstraintsAggregator*/)
        {
            //this.weatherService = weatherService;
            //this.packingTool = packingTool;
            //this.packingListGenerator = packingListGenerator;
            //this.promptProvider = promptProvider;
            //this.kernelInvoker = kernelInvoker;
            this.packingListService = packingListService;
            //this.packingConstraintsAggregator = packingConstraintsAggregator;
        }

        /// <summary>
        /// This endpoint takes in a PackingChatRequest containing a user's message 
        /// and will return a recommended packing list
        /// The endpoint handles multi-turn scenarios
        /// </summary>
        /// <param name="packingChatRequest"></param>
        /// <returns></returns>
        [HttpPost("chat")]
        public async Task<IActionResult> Chat(
            [FromBody] PackingChatRequest packingChatRequest,
            [FromHeader(Name = "X-Conversation-Id")] string conversationId)
        {
            var result = await packingListService.GeneratePackingListAsync(packingChatRequest, conversationId);

            if (result.NeedsClarification)
            {
                return Ok(new { Message = result.ClarificationQuestion });
            }

            if (!string.IsNullOrEmpty(result.Error))
            {
                return StatusCode(500, result.Error);
            }

            return Ok(new { Message = result.FormattedPackingList });


            //// Call the LLM (via the Orchestrator) to get the PackingIntentResult
            //var packingIntentResult = await packingTool.GetPackingIntentAsync(packingChatRequest.Message);

            //// If clarification is needed, return immediately with clarification question
            //if (packingIntentResult.NeedsClarification)
            //{
            //    return Ok(new PackingChatResponse
            //    {
            //        NeedsClarification = true,
            //        ClarificationQuestion = packingIntentResult.ClarificationQuestion ?? "Temporary clarification question",
            //    });
            //}

            //if (string.IsNullOrEmpty(packingIntentResult.Location))
            //{
            //    return Ok(new PackingChatResponse
            //    {
            //        NeedsClarification = true,
            //        ClarificationQuestion = "Could you clarify what your travel destination is?",
            //    });
            //}

            //if (!packingIntentResult.StartTime.HasValue ||  !packingIntentResult.EndTime.HasValue)
            //{
            //    return Ok(new PackingChatResponse
            //    {
            //        NeedsClarification = true,
            //        ClarificationQuestion = "Could you clarify your travel dates?",
            //    });
            //}

            //// Use packingIntentResult to get the Weather Profile from the Weather Service (powered by Open-Meteo APIs)
            //var weatherProfile = await weatherService.GetWeatherProfile(packingIntentResult.Location, packingIntentResult.StartTime.Value, packingIntentResult.EndTime.Value);

            //// Use weatherProfile and activities from packingIntentResult to get the packingConstraints
            //var packingConstraints = packingConstraintsAggregator.BuildPackingConstraints(weatherProfile, packingIntentResult.Activities);

            //// Use the packingConstraints to build the packing list
            //var packingList = packingListGenerator.GeneratePackingList(
            //    packingConstraints, 
            //    packingIntentResult.Location, 
            //    packingIntentResult.StartTime.Value, 
            //    packingIntentResult.EndTime.Value, 
            //    packingIntentResult.Activities);

            //var readoutPrompt = promptProvider.GetPackingListReadoutPrompt();

            //var parameters = new Dictionary<string, object>
            //{
            //    ["location"] = packingIntentResult.Location,
            //    ["startTime"] = $"{packingIntentResult.StartTime:yyyy-MM-dd}",
            //    ["endTime"] = $"{packingIntentResult.EndTime:yyyy-MM-dd}",
            //    ["activities"] = string.Join(", ", packingIntentResult.Activities),
            //    ["packingListJson"] = JsonSerializer.Serialize(packingList)
            //};

            //string message;

            //try
            //{
            //    message = await kernelInvoker.InvokeAsync(readoutPrompt, parameters);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"LLM failed to format response: {ex.Message}");
            //}

            //return Ok(new { Message = message });
        }
    }
}
