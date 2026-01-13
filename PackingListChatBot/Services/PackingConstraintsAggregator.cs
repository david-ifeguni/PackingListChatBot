using PackingListChatBot.Models;

namespace PackingListChatBot.Services
{
    /// <summary>
    /// Builds the packing constraints object
    /// </summary>
    /// <param name="clothingRules"></param>
    /// <param name="activityRules"></param>
    public class PackingConstraintsAggregator(IClothingRules clothingRules, IActivityRules activityRules)
    {
        public PackingConstraints BuildPackingConstraints(WeatherProfile weather, List<TravelActivities> travelActivities)
        {
            var constraints = new PackingConstraints
            {
                Activities = activityRules.Evaluate(travelActivities),
                Clothing = clothingRules.Evaluate(weather)
            };

            return constraints;
        }
    }
}
