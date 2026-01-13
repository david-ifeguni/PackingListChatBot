namespace PackingListChatBot.Services.OpenMeteoService.HistoricalWeather
{
    /// <summary>
    /// The response scheme for the Open-Meteo HistoricalWeather API 
    /// </summary>
    public class HistoricalWeatherResponse
    {
        public double Elevation { get; set; }
        public DailyWeather Daily {  get; set; } = new DailyWeather();
        public HourlyWeather Hourly { get; set; } = new HourlyWeather();
    }

    public class DailyWeather
    {
        public List<string> Time { get; set; } = new List<string>();
        public List<double> Apparent_Temperature_Max { get; set; } = new List<double>();
        public List<double> Apparent_Temperature_Min { get; set; } = new List<double>();
        public List<double> Precipitation_Hours { get; set; } = new List<double>();

    }

    public class HourlyWeather
    {
        public List<double> Relative_Humidity_2m { get; set; } = new List<double>();
    }
}
