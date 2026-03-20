using System.Diagnostics;

namespace PruebaEmi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            
            // Log request
            _logger.LogInformation(
                "Request Middleware: {Method} {Path} from {IP}",
                context.Request.Method,
                context.Request.Path,
                context.Connection.RemoteIpAddress
            );

            await _next(context); // Siguiente middleware

            stopwatch.Stop();
            
            // Log response
            _logger.LogInformation(
                "Response Middleware: {StatusCode} in {ElapsedMs}ms",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds
            );
        }
    }
}