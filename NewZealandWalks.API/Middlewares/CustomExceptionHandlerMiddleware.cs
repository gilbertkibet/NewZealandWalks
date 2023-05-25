using System.Net;

namespace NewZealandWalks.API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //log this exception
                _logger.LogError(ex, $"{errorId}", ex.Message);

                //return custome error response to achieve consistency

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something Went Wrong !"
                };

                await httpContext.Response.WriteAsJsonAsync(error);

            }
        }
    }
}
