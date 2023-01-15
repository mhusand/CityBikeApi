using System.Net;

namespace CityBikeApi.ErrorHandler
{
    public class CityBikeApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ResponseMessage { get; set; }
        public string Title { get; set; } = "Error From gbfs";
        public CityBikeApiException(HttpStatusCode statusCode, string? responseMessage)
        {
            StatusCode = statusCode;
            ResponseMessage = responseMessage;
        }
    }
}
