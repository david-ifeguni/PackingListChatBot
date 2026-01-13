using PackingListChatBot.Models;

namespace PackingListChatBot.Services
{
    /// <summary>
    /// Sets the clothing constraints based off the WeatherProfile
    /// </summary>
    public class ClothingRules : IClothingRules
    {
        public ClothingConstraints Evaluate(WeatherProfile weather)
        {
            ClothingConstraints clothingConstraints = new ClothingConstraints();

            if (weather.AverageLowF <= 50)
            {
                clothingConstraints.NeedsWarmLayers = true;
                clothingConstraints.Notes.Add("Expected to be cold during day and night");
            }

            if (weather.NumOfRainDays >= 3)
            {
                clothingConstraints.NeedsRainProtection = true;
                clothingConstraints.Notes.Add("Expected to be multiple days of rain");
            }

            if (weather.AverageHighF >= 75)
            {
                clothingConstraints.NeedsBreathableClothing = true;
                clothingConstraints.Notes.Add("Expected to have warm days");
            }

            if (weather.HumidityPercentage >= 70)
            {
                clothingConstraints.NeedsBreathableClothing = true;
                clothingConstraints.Notes.Add("Expected to have high humidity days");
            }

            if (weather.ElevationLevelMeters >= 1500)
            {
                clothingConstraints.NeedsSunProtection = true;
                clothingConstraints.Notes.Add("Expected to be in a place with higher elevaton, increases UV exposure");
            }

            if (weather.AverageHighF - weather.AverageLowF >= 15)
            {
                clothingConstraints.LargeTempSwing = true;
                clothingConstraints.Notes.Add("Expected to have large swings between the high and low temperatures");
            }

            return clothingConstraints;
        }
    }
}
