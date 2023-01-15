using System.Net;

namespace CityBikeApi.ErrorHandler
{
    public class ErrorHandler
    {
        private readonly RequestDelegate next;
        public ErrorHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var errorDetails = new ErrorDetails();
                switch (exception)
                {
                    case BadHttpRequestException badHttpRequestException:
                        errorDetails.Title = "Bad Request";
                        errorDetails.Status = (int)HttpStatusCode.BadRequest;
                        errorDetails.Detail = badHttpRequestException.Message;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsJsonAsync(errorDetails);
                        break;

                    case TaskCanceledException taskCanceledException:
                        errorDetails.Title = "Timeout";
                        errorDetails.Status = (int)HttpStatusCode.GatewayTimeout;
                        errorDetails.Detail = taskCanceledException.Message;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsJsonAsync(errorDetails);
                        break;

                    case CityBikeApiException cityBikeApiException:
                        errorDetails.Title = cityBikeApiException.Title;
                        errorDetails.Status = (int)cityBikeApiException.StatusCode;
                        errorDetails.Detail = cityBikeApiException.ResponseMessage;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsJsonAsync(errorDetails);
                        break;

                    default:
                        const string defaultTekst = "UnexpectedError";
                        errorDetails.Title = defaultTekst;
                        errorDetails.Status = (int)HttpStatusCode.InternalServerError;
                        errorDetails.Detail = exception.ToString();
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsJsonAsync(errorDetails);
                        break;
                }
            }
        }
    }
}
