using PackingListChatBot.Models;

namespace PackingListChatBot.Services
{
    public interface IWeatherService
    {
        Task<WeatherProfile> GetWeatherProfile(string location, DateOnly startTime, DateOnly endTime);
    }
}
