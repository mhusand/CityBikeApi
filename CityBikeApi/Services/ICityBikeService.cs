using CityBikeApi.CityBikeDtos;

namespace CityBikeApi.Services
{
    public interface ICityBikeService
    {
        Task<List<GetStationStatusDto>> GetListStationStatus();
    }
}
