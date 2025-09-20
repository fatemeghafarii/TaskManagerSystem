using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace TaskManager.Common.Infrastructure.Middlewares;
public class RequestResponseLoggingMiddleware
{

    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 📌 لاگ Request
        context.Request.EnableBuffering(); // اجازه خوندن چندباره body
        var requestBody = string.Empty;
        if (context.Request.ContentLength > 0)
        {
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0; // ریست پوزیشن برای Middlewareهای بعدی
        }

        _logger.LogInformation("➡️ Request {method} {url} Body: {body}",
            context.Request.Method,
            context.Request.Path,
            requestBody);

        var stopwatch = Stopwatch.StartNew();

        // 📌 گرفتن Response
        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context); // بقیه middleware ها + controller

        stopwatch.Stop();

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        _logger.LogInformation("⬅️ Response {statusCode} in {ms}ms Body: {body}",
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds,
            responseBody);

        await responseBodyStream.CopyToAsync(originalBodyStream);
    }
}

// 📌 Extension برای ثبت در Program.cs
public static class RequestResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}

