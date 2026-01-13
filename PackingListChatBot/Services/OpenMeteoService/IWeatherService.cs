using PackingListChatBot.Models;

namespace PackingListChatBot.Services.OpenMeteoService
{
    public interface IWeatherService
    {
        Task<WeatherProfile> GetWeatherProfile(string location, DateOnly startTime, DateOnly endTime);
    }
}
