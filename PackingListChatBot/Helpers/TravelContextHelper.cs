using PackingListChatBot.Models;
using PackingListChatBot.SemanticKernel.Models;

namespace PackingListChatBot.Helpers
{
    public class TravelContextHelper
    {
        public static void Update(TravelContext travelContext, PackingIntentResult packingIntentResult)
        {
            if (!string.IsNullOrEmpty(packingIntentResult.Location))
            {
                travelContext.Location = packingIntentResult.Location;
            }
            if (packingIntentResult.StartTime.HasValue)
            {
                travelContext.StartTime = packingIntentResult.StartTime.Value;
            }
            if (packingIntentResult.EndTime.HasValue)
            {
                travelContext.EndTime = packingIntentResult.EndTime.Value;
            }
            foreach (var activity in packingIntentResult.Activities!)
            {
                travelContext.Activities.Add(activity);
            }
        }

        public static string GetClarificationQuestion(TravelContext travelContext)
        {
            if (string.IsNullOrEmpty(travelContext.Location))
            {
                return "Where are you travelling to?";
            }
            if (!travelContext.StartTime.HasValue || !travelContext.EndTime.HasValue)
            {
                return "When dates are you thinking of travelling?";
            }
            return "Do you have any activities planned?";
        }
    }
}
