using CityBikeApi.CityBikeDtos;
using CityBikeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CityBikeApi.Controllers
{
    [Route("api/citybike")]
    [ApiController]
    public class CityBikeController : ControllerBase
    {
        private readonly ICityBikeService cityBikeService;
        public CityBikeController(ICityBikeService cityBikeService)
        {
            this.cityBikeService = cityBikeService;
        }
        [HttpGet("getstationinformation")]
        [SwaggerOperation(
        Summary = "Henter en liste med antall ledige stativer og sykler for alle stasjoner.",
        Tags = new[] { "Station" })
        ]
        public async Task<ActionResult<GetStationStatusDto>> GetListStationStatus()
        {
            var clients = await cityBikeService.GetListStationStatus();
            return Ok(clients);
        }
    }
}