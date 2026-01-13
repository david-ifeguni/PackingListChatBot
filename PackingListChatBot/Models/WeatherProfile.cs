namespace PackingListChatBot.Models
{
    /// <summary>
    /// Defines the weather information for the specified location during the requested timeframe
    /// </summary>
    public class WeatherProfile
    {
        public string Location { get; set; } = string.Empty;
        public double AverageHighF {  get; set; }
        public double AverageLowF { get; set; }
        public int NumOfRainDays { get; set; }
        public double HumidityPercentage { get; set; }
        public double ElevationLevelMeters { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
