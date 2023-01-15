using System.Text.Json.Serialization;

namespace CityBikeApi.ErrorHandler
{
    public class ErrorDetails
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }
        [JsonPropertyName("detail")]
        public string? Detail { get; set; }
    }
}
