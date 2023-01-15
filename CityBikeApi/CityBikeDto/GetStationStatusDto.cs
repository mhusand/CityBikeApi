namespace CityBikeApi.CityBikeDtos
{
    public class GetStationStatusDto
    {
        public int station_id { get; init; } = 0;
        public string? name { get; init; } = string.Empty;


        public string address { get; init; } = string.Empty;
        public int capacity { get; init; } = 0;

        public int num_bikes_available { get; init; } = 0;
        public int num_docks_available { get; init; } = 0;
    }
}
