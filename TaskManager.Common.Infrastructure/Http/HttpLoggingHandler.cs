using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TaskManager.Common.Infrastructure.Http;
public class HttpLoggingHandler : DelegatingHandler
{
    private readonly ILogger<HttpLoggingHandler> _logger;

    public HttpLoggingHandler(ILogger<HttpLoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // لاگ درخواست
        _logger.LogInformation("HTTP Request: {method} {url}", request.Method, request.RequestUri);

        if (request.Content != null)
        {
            var requestBody = await request.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Request Body: {body}", requestBody);
        }

        var stopwatch = Stopwatch.StartNew();
        var response = await base.SendAsync(request, cancellationToken);
        stopwatch.Stop();

        // لاگ پاسخ
        _logger.LogInformation("HTTP Response: {statusCode} in {ms}ms", response.StatusCode, stopwatch.ElapsedMilliseconds);

        if (response.Content != null)
        {
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("Response Body: {body}", responseBody);
        }

        return response;
    }
}
