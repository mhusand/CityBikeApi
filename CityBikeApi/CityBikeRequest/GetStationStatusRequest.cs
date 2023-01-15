namespace CityBikeApi.CityBikeDtos
{
  
        public class GetStationStatusRequest
    {
            public GetStationStatus data { get; init; } = new GetStationStatus();
        }

        public class GetStationStatus
        {
            public List<StationStatus> stations { get; init; } = new List<StationStatus>();
        }
        public class StationStatus
        {
            public int station_id { get; init; } = 0;
            public int num_bikes_available { get; init; } = 0;
            public int num_docks_available { get; init; } = 0;
        }
    
}
