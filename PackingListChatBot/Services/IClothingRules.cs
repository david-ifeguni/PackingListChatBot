using PackingListChatBot.Models;

namespace PackingListChatBot.Services
{
    public interface IClothingRules
    {
        ClothingConstraints Evaluate(WeatherProfile weather);
    }
}
