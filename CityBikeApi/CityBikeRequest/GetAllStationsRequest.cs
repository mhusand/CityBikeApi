namespace CityBikeApi.CityBikeDtos
{
    public class GetAllStationsRequest
    {
        public GetAllStationsDto data { get; init; } = new GetAllStationsDto();
    }
    public class GetAllStationsDto 
    {
        public List<StationInformationResponse> stations { get; init; } = new List<StationInformationResponse>();
    }
    public class StationInformationResponse
    {
        public int station_id { get; init; } = 0;
        public string? name { get; init; } = string.Empty;
        public string address { get; init; } = string.Empty;
        public int capacity { get; init; } = 0;
    }
}
