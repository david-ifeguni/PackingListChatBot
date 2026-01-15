using PackingListChatBot.Models;
using PackingListChatBot.SemanticKernel.Models;

namespace PackingListChatBot.Helpers
{
    public class TravelContextHelper
    {
        /// <summary>
        /// Updates the current TravelContext object with the latest PackingIntent results
        /// </summary>
        /// <param name="travelContext"></param>
        /// <param name="packingIntentResult"></param>
        public static void Update(TravelContext travelContext, PackingIntentResult packingIntentResult)
        {
            if (!string.IsNullOrEmpty(packingIntentResult.Location))
            {
                travelContext.Location = packingIntentResult.Location.Trim();
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

        /// <summary>
        /// Gets the necessary clarification question to complete TravelContext 
        /// Currently done incrementally (ie. one question at a time)
        /// </summary>
        /// <param name="travelContext"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines if the user's latest message (via PackingIntentResult) is about 
        /// current TravelContext or a new one
        /// </summary>
        /// <param name="travelContext"></param>
        /// <param name="packingIntentResult"></param>
        /// <returns></returns>
        public static bool IsNewTrip(TravelContext travelContext, PackingIntentResult packingIntentResult)
        {
            if (string.IsNullOrEmpty(packingIntentResult.Location) || string.IsNullOrEmpty(travelContext.Location))
            {
                return false;
            }

            return !string.Equals(travelContext.Location,
                packingIntentResult?.Location, StringComparison.OrdinalIgnoreCase);
        }
    }
}
