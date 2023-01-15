using CityBikeApi.CityBikeDtos;
using CityBikeApi.ErrorHandler;

namespace CityBikeApi.Services
{
    public class CityBikeService : ICityBikeService
    {
        private readonly HttpClient _httpClient;
        public CityBikeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<StationInformationResponse>?> GetAllStations()
        {
            var stations = await Get<GetAllStationsRequest>(ApiPath.StationInformation);
            return stations?.data?.stations;
        }
        public async Task<List<StationStatus>?> GetStationsStatus()
        {
            var stations = await Get<GetStationStatusRequest>(ApiPath.StationInformation);
            return stations?.data?.stations;
        }
        public async Task<List<GetStationStatusDto>> GetListStationStatus()
        {
            var stations = await GetAllStations();
            var stationStatus = await GetStationsStatus();
            var stationstatuslist = new List<GetStationStatusDto>();
            if (stationStatus == null)
            {
                return new List<GetStationStatusDto>();
            }
            foreach (var station in stationStatus)
            {
                var stationinfo = stations?.SingleOrDefault(x => x.station_id == station.station_id);
                if (stationinfo == null)
                {
                    continue;
                }
                stationstatuslist.Add(new GetStationStatusDto
                {
                    station_id = station.station_id,
                    name = stationinfo.name,
                    address = stationinfo.address,
                    capacity = stationinfo.capacity,
                    num_bikes_available = station.num_bikes_available,
                    num_docks_available = station.num_docks_available
                });
            }
            return stationstatuslist;
        }


        private async Task<T?> Get<T>(string url)
        {
            using var httpResponse = await _httpClient.GetAsync(url);

            if (httpResponse == null)
            {
                throw new Exception($"Feil i http GET kall til {url}");
            }

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await httpResponse.Content.ReadFromJsonAsync<T>();
                return response;
            }
            else
            {
                throw new CityBikeApiException(httpResponse.StatusCode, httpResponse.ReasonPhrase);
            }
        }
    }
}
