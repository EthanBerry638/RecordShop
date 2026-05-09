using System.Diagnostics;

namespace RecordShop.Api.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch sw = Stopwatch.StartNew();

            _logger.LogInformation("Handling request: {Method} {Path}",
                context.Request.Method, context.Request.Path);

            await next(context);

            sw.Stop();

            _logger.LogInformation("Handling response: Status Code: {StatusCode}. Finished in: {Ms}ms",
                context.Response.StatusCode, sw.ElapsedMilliseconds);
        }
    }
}
