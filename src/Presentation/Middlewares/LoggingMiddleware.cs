using System.Diagnostics;

namespace motcyApi.Presentation.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred: {ex.Message}");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Response: {context.Response.StatusCode} | Time Taken: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
