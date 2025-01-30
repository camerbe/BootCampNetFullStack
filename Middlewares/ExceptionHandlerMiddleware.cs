using System.Net;

namespace BootCampNetFullStack.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(
            ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next
            )
        {
            _logger = logger;
            _next = next;
        }

        public async Task Handle(HttpContext httpContext)
        {
            try { 
               await _next(httpContext);
            }
            catch(Exception ex) { 
                var errorId=Guid.NewGuid().ToString();

                _logger.LogError(ex, $"{errorId} {ex.Message}");

                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError; 
                httpContext.Response.ContentType="application/json";

                var error=new
                {
                    errorId = errorId,
                    erroMessage = $"Quelque chose s'est mal passé. Nous cherchons à résoudre ce problème..."
                };
                await httpContext.Response.WriteAsJsonAsync( error ); 
            }

        
        }
        
    }
}
