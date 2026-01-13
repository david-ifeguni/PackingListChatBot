using Microsoft.AspNetCore.Mvc;
using PackingListChatBot.Models;
using PackingListChatBot.Services.Packing;

namespace PackingListChatBot.Controllers
{
    [ApiController]
    [Route("api/packing")]
    public class PackingListController : ControllerBase
    {
        private readonly IPackingListService packingListService;

        public PackingListController(
            IPackingListService packingListService)
        {
            this.packingListService = packingListService;
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
        }
    }
}
