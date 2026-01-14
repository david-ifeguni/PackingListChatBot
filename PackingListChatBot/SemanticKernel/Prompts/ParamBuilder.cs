using System.Diagnostics;
using System.Text.Json;
using PackingListChatBot.Models;

namespace PackingListChatBot.SemanticKernel.Prompts
{
    public class ParamBuilder
    {
        public static Dictionary<string, object> BuildPackingListReadoutParams(WeatherProfile weatherProfile, PackingList packingList)
        {
            var parameters = new Dictionary<string, object>
            {
                ["location"] = weatherProfile.Location,
                ["startTime"] = $"{packingList.StartTime:yyyy-MM-dd}",
                ["endTime"] = $"{packingList.EndTime:yyyy-MM-dd}",
                ["activities"] = string.Join(", ", packingList.Activities),
                ["packingListJson"] = JsonSerializer.Serialize(packingList),
                ["temperatureRange"] = $"from {weatherProfile.AverageLowF}°F to {weatherProfile.AverageHighF}°F",
                ["rainLikelihood"] = RainLikelihood(weatherProfile.NumOfRainDays, packingList.StartTime, packingList.EndTime),
                ["humiditySummary"] = HumiditySummary(weatherProfile.HumidityPercentage),
                ["elevationSummary"] = ElevationSummary(weatherProfile.ElevationLevelMeters)
            };


            return parameters;
        }

        public static string RainLikelihood(int numOfRainDays, DateOnly startTime, DateOnly endTime)
        {
            double val = numOfRainDays / (double)endTime.DayNumber - startTime.DayNumber;
            switch (val)
            {
                case <= 0.25:
                    return "rain is very unlikely";
                case <= 0.50:
                    return "rain is fairly likely";
                case <= 0.75:
                        return "rain is likely";
            }
            return "return is very likely";
        }

        public static string HumiditySummary(double humidityPercentage)
        {
            switch (humidityPercentage)
            {
                case <= 50:
                    return "generally low humidity";
                case <= 70:
                    return "moderate humidity";
            }
            return "generally high humidity";
        }

        public static string ElevationSummary(double elevationLevelMeters)
        {
            return elevationLevelMeters >= 1500 ?
                "high elevation, can make temperatures feel cooler and increase sun exposure" :
                "elevation not expected to impact weather conditions";
        }
    }
}
