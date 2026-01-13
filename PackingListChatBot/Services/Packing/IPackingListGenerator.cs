using PackingListChatBot.Models;

namespace PackingListChatBot.Services.Packing
{
    public interface IPackingListGenerator
    {
        PackingList GeneratePackingList(PackingConstraints packingConstraints, string location, DateOnly startTime, DateOnly endTime, List<TravelActivities> activities);
    }
}
