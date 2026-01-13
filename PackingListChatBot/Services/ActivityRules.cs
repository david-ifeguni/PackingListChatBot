using PackingListChatBot.Models;

namespace PackingListChatBot.Services
{
    /// <summary>
    /// Sets the activity constraints based off a list of requested travel activities
    /// </summary>
    public class ActivityRules : IActivityRules
    {
        public ActivityConstraints Evaluate(List<TravelActivities> travelActivities)
        {
            ActivityConstraints activityConstraints = new ActivityConstraints();

            if (travelActivities.Contains(TravelActivities.CityWalking) ||
                travelActivities.Contains(TravelActivities.Hiking) ||
                travelActivities.Contains(TravelActivities.Museum) ||
                travelActivities.Contains(TravelActivities.Zoo))
            {
                activityConstraints.NeedsComfortableWalkingFootwear = true;
                activityConstraints.Notes.Add("Expected to do extended walking");
            }

            if (travelActivities.Contains(TravelActivities.Beach))
            {
                activityConstraints.NeedsSwimwear = true;
                activityConstraints.NeedsChangeOfClothes = true;
                activityConstraints.Notes.Add("Expected to do activities at the beach");
            }

            if (travelActivities.Contains(TravelActivities.Nightlife) ||
                travelActivities.Contains(TravelActivities.WineTasting) ||
                travelActivities.Contains(TravelActivities.IndoorDining) ||
                travelActivities.Contains(TravelActivities.OutdoorDining))
            {
                activityConstraints.NeedsFormalWear = true;
                activityConstraints.Notes.Add("Expected to have evening and formal/upscale events");
            }

            if (travelActivities.Contains(TravelActivities.Rafting))
            {
                activityConstraints.NeedsWaterProofFootwear = true;
                activityConstraints.NeedsQuickDryClothing = true;
                activityConstraints.Notes.Add("Expected to get wet during activity due to rafting");
            }

            if (travelActivities.Contains(TravelActivities.Hiking) ||
                travelActivities.Contains(TravelActivities.Rafting))
            {
                activityConstraints.NeedsAthleticWear = true;
                activityConstraints.NeedsWeatherFlexibleWear = true;
                activityConstraints.Notes.Add("Expected to do a lot of physical activity");
            }

            return activityConstraints;
        }
    }
}
