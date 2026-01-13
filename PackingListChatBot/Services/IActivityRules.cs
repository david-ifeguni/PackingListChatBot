using PackingListChatBot.Models;

namespace PackingListChatBot.Services
{
    public interface IActivityRules
    {
        ActivityConstraints Evaluate(List<TravelActivities> travelActivities);
    }
}
