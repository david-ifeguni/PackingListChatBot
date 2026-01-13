namespace PackingListChatBot.Services.OpenMeteoService.Geocoding
{
    /// <summary>
    /// The response scheme for the Open-Meteo Geocoding API 
    /// </summary>
    public class GeocodingResponse
    {
        public List<GeocodingResult> Results { get; set; } = new List<GeocodingResult>();
    }

    public class GeocodingResult
    {
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public string Admin1 { get; set; } = string.Empty;
        public string Country {  get; set; } = string.Empty;
    }
}
