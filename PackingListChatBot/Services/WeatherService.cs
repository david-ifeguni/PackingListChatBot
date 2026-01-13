using PackingListChatBot.Models;
using PackingListChatBot.Services.OpenMeteoService.Geocoding;
using PackingListChatBot.Services.OpenMeteoService.HistoricalWeather;


namespace PackingListChatBot.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private const string geocodingUrl = "https://geocoding-api.open-meteo.com/v1/search";
        private const string historicalWeatherUrl = "https://archive-api.open-meteo.com/v1/archive";

        public WeatherService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Calls the Open-Meteo APIs and returns a weather profile
        /// If an error occurs, throw an exception
        /// </summary>
        public async Task<WeatherProfile> GetWeatherProfile(string location, DateOnly startDate, DateOnly endDate)
        {
            var geocodingResponse = await httpClient.GetFromJsonAsync<GeocodingResponse>($"{geocodingUrl}?name={location}&count=1");

            var geocodingResult = geocodingResponse?.Results?.FirstOrDefault() ?? throw new Exception("No location found");

            // We have a valid location (aka latitude and longitude), now we can get the historical weather information

            double lat = geocodingResult.Latitude;
            double lng = geocodingResult.Longitude;
            var historicalStart = $"{startDate.AddYears(-1):yyyy-MM-dd}";
            var historicalEnd = $"{endDate.AddYears(-1):yyyy-MM-dd}";

            var historicalWeatherResponse = await httpClient.GetFromJsonAsync<HistoricalWeatherResponse>(historicalWeatherUrl +
                                                                                                $"?latitude={lat}&longitude={lng}" +
                                                                                                $"&start_date={historicalStart}" +
                                                                                                $"&end_date={historicalEnd}" +
                                                                                                $"&daily=apparent_temperature_max,apparent_temperature_min,rain_sum,precipitation_hours,weather_code" +
                                                                                                $"&hourly=relative_humidity_2m" +
                                                                                                $"&temperature_unit=fahrenheit" +
                                                                                                $"&timezone=GMT");

            if (historicalWeatherResponse == null)
            {
                throw new Exception("Weather information unavailable");
            }

            string officialLocation = string.Concat(geocodingResult.Name, ",", geocodingResult.Admin1, ",", geocodingResult.Country);
            // Potential problem with above is that Admin1 may be null/empty sometimes
            var weatherProfile = new WeatherProfile
            {
                Location = officialLocation,
                AverageHighF = historicalWeatherResponse.Daily.Apparent_Temperature_Max.Average(),
                AverageLowF = historicalWeatherResponse.Daily.Apparent_Temperature_Min.Average(),
                NumOfRainDays = historicalWeatherResponse.Daily.Precipitation_Hours.Count(x => x > 0),
                ElevationLevelMeters = historicalWeatherResponse.Elevation,
                HumidityPercentage = historicalWeatherResponse.Hourly.Relative_Humidity_2m.Average(),
                Notes = $"Historical weather information for {officialLocation} from Open-Meteo"
            };

            return weatherProfile;
        }
    }
}
