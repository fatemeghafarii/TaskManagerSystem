using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TaskManager.Common.Persistence.EF.FrameworkDbContext;
using TaskManager.Tasks.Domain.Events;

namespace TaskManager.Tasks.API.BackgroundServices;
public class EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger, IHttpClientFactory httpClientFactory) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<EventDispatcher> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<FrameworkContext>();

                var pendingEvents = await context.OutboxEventItems
                                                                  .Where(e => e.PublishedOn == null)
                                                                  .ToListAsync(stoppingToken);

                foreach (var ev in pendingEvents)
                {
                    var domainEvent = JsonSerializer.Deserialize<TaskStatusChangedEvent>(ev.Payload!); // Payload فیلدی که JSON رو نگه می‌داره

                    if (domainEvent == null)
                        continue;

                    // فعلاً با HttpClient → بعداً می‌تونی Kafka/RabbitMQ بذاری
                    var client = _httpClientFactory.CreateClient("HistoryService");
                    var response = await client.PostAsJsonAsync(
                                                                "api/events/task-status-changed", 
                                                                ev, 
                                                                stoppingToken
                                                                );

                    if (response.IsSuccessStatusCode)
                    {
                        ev.PublishedOn = DateTime.UtcNow;
                        _logger.LogInformation($" Event {ev.Id} published to HistoryService."); //✅
                    }
                    else
                    {
                        _logger.LogWarning($" Failed to publish event {ev.Id}. Status: {response.StatusCode}"); //❌
                    }
                }

                await context.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while dispatching events.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // هر ۵ ثانیه چک کنه
        }
    }
}